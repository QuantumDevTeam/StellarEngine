import os
import sys
from collections import defaultdict

try:
    import pathspec
except ImportError:
    print("Ошибка: требуется библиотека 'pathspec'. Установите её командой: pip install pathspec")
    sys.exit(1)


def load_gitignore_spec(dir_path):
    gitignore_path = os.path.join(dir_path, '.gitignore')
    if not os.path.isfile(gitignore_path):
        return None

    with open(gitignore_path, 'r', encoding='utf-8') as f:
        lines = []
        for line in f:
            line = line.strip()
            if line and not line.startswith('#'):
                lines.append(line)
    if not lines:
        return None
    return pathspec.PathSpec.from_lines('gitwildmatch', lines)


def is_ignored(file_path, root_dir, spec_cache):
    rel_path = os.path.relpath(file_path, root_dir)
    parts = rel_path.split(os.sep)
    is_dir = os.path.isdir(file_path)
    check_path = rel_path + '/' if is_dir else rel_path

    for i in range(len(parts) + 1):
        parent = os.path.join(root_dir, *parts[:i])
        if parent in spec_cache:
            spec = spec_cache[parent]
        else:
            spec = load_gitignore_spec(parent)
            spec_cache[parent] = spec

        if spec is not None:
            subpath = os.path.relpath(file_path, parent)
            if is_dir:
                subpath += '/'
            if spec.match_file(subpath):
                return True
    return False


def count_lines(file_path):
    try:
        with open(file_path, 'r', encoding='utf-8', errors='ignore') as f:
            return sum(1 for _ in f)
    except Exception as e:
        print(f"Предупреждение: не удалось прочитать {file_path} - {e}", file=sys.stderr)
        return 0


def main():
    script_dir = os.path.dirname(os.path.abspath(__file__))
    root_dir = script_dir
    os.chdir(root_dir)

    print(f"Сканирование папки: {root_dir}\n")

    spec_cache = {}
    results = []
    total_files = 0
    total_lines = 0

    for current_dir, dirs, files in os.walk('.'):
        if current_dir == '.':
            current_dir_rel = ''
        else:
            current_dir_rel = current_dir[2:]

        if '.git' in dirs:
            dirs.remove('.git')

        i = 0
        while i < len(dirs):
            subdir = dirs[i]
            full_subdir = os.path.join(current_dir, subdir)
            if is_ignored(full_subdir, root_dir, spec_cache):
                dirs.pop(i)
            else:
                i += 1

        for file in files:
            full_path = os.path.join(current_dir, file)
            if file == '.gitignore':
                continue

            if is_ignored(full_path, root_dir, spec_cache):
                continue

            lines = count_lines(full_path)
            rel_display = os.path.join(current_dir_rel, file) if current_dir_rel else file
            results.append((rel_display, lines))
            total_files += 1
            total_lines += lines

    results.sort(key=lambda x: x[0])

    for file_path, line_count in results:
        print(f"Файл: {file_path}, строк: {line_count}")

    print("\n" + "=" * 40)
    print(f"Всего файлов: {total_files}")
    print(f"Всего строк: {total_lines}")


if __name__ == "__main__":
    main()
