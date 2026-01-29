using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Stellar.Kernel.EntryPoint;

namespace Stellar.Sdk.Tasks
{
    public class StellarEntryPointFind : Task
    {
        [Required] public ITaskItem[] CompileItems { get; set; }
        [Required] public string ProjectDirectory { get; set; }

        [Output] public ITaskItem StellarEntryPoint { get; set; }

        public override bool Execute()
        {
            try
            {
                if (CompileItems == null || CompileItems.Length == 0)
                {
                    Log.LogError("No compile items found. The project might not have any .cs files.");
                    return false;
                }

                var possibleEntryPoints = new List<ClassDeclarationSyntax>();

                foreach (var compileItem in CompileItems)
                {
                    var filePath = compileItem.ItemSpec;
                    string fullPath = Path.IsPathRooted(filePath) ? filePath : Path.Combine(ProjectDirectory, filePath);

                    if (!File.Exists(fullPath)) continue;

                    var sourceCode = File.ReadAllText(fullPath);
                    var tree = CSharpSyntaxTree.ParseText(sourceCode);
                    var root = tree.GetRoot();

                    var classesWithAttribute = root.DescendantNodes()
                        .OfType<ClassDeclarationSyntax>()
                        .Where(c => IsClassMarkedWithStellarEntryPoint(c, typeof(StellarEntryPointAttribute)))
                        .ToList();

                    var entryPointClasses = classesWithAttribute
                        .Where(c => IsImplementsStellarEntryPointInterface(c, typeof(IStellarEntryPoint)))
                        .ToList();

                    possibleEntryPoints.AddRange(entryPointClasses);
                    if (possibleEntryPoints.Count <= 1) continue;
                    Log.LogError("Multiple entry points found. The project should have only one entry point.");
                    return false;
                }

                if (possibleEntryPoints.Count == 1)
                {
                    StellarEntryPoint = new TaskItem(GetFullClassName(possibleEntryPoints.First()));
                    Log.LogMessage(MessageImportance.High, $"Found entry point: `{StellarEntryPoint.ItemSpec}`");
                    return true;
                }

                Log.LogError(
                    possibleEntryPoints.Count == 0
                        ? "The project does not have an entry point. Make sure you have a class with [StellarEntryPoint] attribute implementing IStellarEntryPoint interface."
                        : "The project should have only one entry point.");
                return false;
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }
        }

        private string GetFullClassName(ClassDeclarationSyntax classDecl)
        {
            try
            {
                var namespaceParts = new List<string>();
                var typeParts = new List<string> { classDecl.Identifier.Text };

                SyntaxNode current = classDecl;

                while (current.Parent != null)
                {
                    current = current.Parent;

                    if (current is NamespaceDeclarationSyntax namespaceDecl)
                    {
                        namespaceParts.Add(namespaceDecl.Name.ToString());
                    }
                    else if (current is ClassDeclarationSyntax parentClass)
                    {
                        typeParts.Insert(0, parentClass.Identifier.Text);
                    }
                    else if (current is StructDeclarationSyntax parentStruct)
                    {
                        typeParts.Insert(0, parentStruct.Identifier.Text);
                    }
                    else if (current is RecordDeclarationSyntax parentRecord)
                    {
                        typeParts.Insert(0, parentRecord.Identifier.Text);
                    }
                }

                if (namespaceParts.Count > 0)
                {
                    var namespaceString = string.Join(".", namespaceParts);
                    return $"{namespaceString}.{string.Join(".", typeParts)}";
                }

                return string.Join(".", typeParts);
            }
            catch (Exception ex)
            {
                Log.LogMessage(MessageImportance.Normal, $"Error getting full class name: {ex.Message}");
                // Возвращаем хотя бы имя класса, если не удалось получить namespace
                return classDecl.Identifier.Text;
            }
        }

        private bool IsClassMarkedWithStellarEntryPoint(ClassDeclarationSyntax classDeclaration, Type attributeType)
        {
            var attributes = classDeclaration.AttributeLists
                .SelectMany(al => al.Attributes);

            return attributes.Any(attr =>
            {
                var attrName = attr.Name.ToString();
                return attrName == attributeType.Name ||
                       attrName == attributeType.Name.Replace("Attribute", "") ||
                       attrName == attributeType.Name + "Attribute";
            });
        }

        private bool IsImplementsStellarEntryPointInterface(ClassDeclarationSyntax classDeclaration, Type interfaceType)
        {
            if (classDeclaration.BaseList == null)
                return false;

            if (classDeclaration.BaseList.Types
                .Any(t => t.Type.ToString() == interfaceType.Name))
                return true;

            return classDeclaration.BaseList.Types
                .Select(baseType => baseType.Type.ToString())
                .Select(parentClassName => classDeclaration.SyntaxTree.GetRoot()
                    .DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .FirstOrDefault(c => c.Identifier.Text == parentClassName))
                .Where(parentClass => parentClass != null)
                .Any(parentClass => IsImplementsStellarEntryPointInterface(parentClass, interfaceType));
        }
    }
}