﻿// <copyright>
// Copyright 2020 Max Ieremenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceModel.Grpc.DesignTime.Generator.Internal.CSharp;

namespace ServiceModel.Grpc.DesignTime.Generator
{
    internal readonly ref struct CompilationUnit
    {
        private readonly IList<IDisposable> _indentation;

        public CompilationUnit(ClassDeclarationSyntax node)
        {
            Output = new CodeStringBuilder();
            _indentation = new List<IDisposable>();

            DeclareClass(node);
        }

        public CodeStringBuilder Output { get; }

        public string GetSourceText(IEnumerable<string> imports)
        {
            for (var i = 0; i < _indentation.Count; i++)
            {
                _indentation[i].Dispose();
                Output.AppendLine("}");
            }

            var text = Output.AsStringBuilder();

            InsertImports(text, imports);
            InsertComment(text);

            return text.ToString();
        }

        private static void InsertComment(StringBuilder text)
        {
            var comment = @"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

"
                .Replace("\r\n", "\n")
                .Replace("\n", Environment.NewLine);

            text.Insert(0, comment);
        }

        private static void InsertImports(StringBuilder text, IEnumerable<string> imports)
        {
            text.Insert(0, Environment.NewLine);

            var distinct = new HashSet<string>(StringComparer.Ordinal);
            foreach (var import in imports.OrderByDescending(i => i, StringComparer.Ordinal))
            {
                if (!distinct.Add(import))
                {
                    continue;
                }

                text.Insert(0, "using " + import + ";" + Environment.NewLine);
            }
        }

        private void DeclareClass(ClassDeclarationSyntax node)
        {
            var owners = ImmutableArray<string>.Empty;

            foreach (var ancestor in node.Ancestors())
            {
                switch (ancestor)
                {
                    case NamespaceDeclarationSyntax ns:
                        owners = owners.Insert(0, "namespace " + ns.Name.WithoutTrivia());
                        break;

                    case ClassDeclarationSyntax c:
                        owners = owners.Insert(0, "partial class " + c.Identifier.WithoutTrivia());
                        break;
                }
            }

            owners = owners.Add("partial class " + node.Identifier.WithoutTrivia());

            for (var i = 0; i < owners.Length; i++)
            {
                Output
                    .AppendLine(owners[i])
                    .AppendLine("{");

                _indentation.Add(Output.Indent());
            }
        }
    }
}
