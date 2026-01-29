import subprocess
import sys


def run_command(cmd, error_msg):
    """Запускает команду и проверяет результат"""
    result = subprocess.run(cmd, stdout=sys.stdout, stderr=sys.stderr)

    if result.returncode != 0:
        raise RuntimeError(f"{error_msg}: {result.returncode}")

    return result.stdout


def main():
    run_command(["package.bat"], "SDK Package script error")


if __name__ == "__main__":
    try:
        main()
    except Exception as e:
        input(f"Any error occured: {e}\npress Enter to exit")
