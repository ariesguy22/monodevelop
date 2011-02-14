// 
// UsingAliasDeclaration.cs
//  
// Author:
//       Mike Krüger <mkrueger@novell.com>
// 
// Copyright (c) 2009 Novell, Inc (http://www.novell.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace MonoDevelop.CSharp.Ast
{
	/// <summary>
	/// using Alias = Import;
	/// </summary>
	public class UsingAliasDeclaration : AstNode
	{
		public static readonly Role<Identifier> AliasRole = new Role<Identifier>("Alias", Identifier.Null);
		public static readonly Role<AstType> ImportRole = UsingDeclaration.ImportRole;
		
		public override NodeType NodeType {
			get {
				return NodeType.Unknown;
			}
		}
		
		public CSharpTokenNode UsingToken {
			get { return GetChildByRole (Roles.Keyword); }
		}
		
		public string Alias {
			get {
				return GetChildByRole (AliasRole).Name;
			}
			set {
				SetChildByRole(AliasRole, new Identifier(value, AstLocation.Empty));
			}
		}
		
		public CSharpTokenNode AssignToken {
			get { return GetChildByRole (Roles.Assign); }
		}
		
		public AstType Import {
			get { return GetChildByRole (ImportRole); }
			set { SetChildByRole (ImportRole, value); }
		}
		
		public CSharpTokenNode SemicolonToken {
			get { return GetChildByRole (Roles.Semicolon); }
		}
		
		public override S AcceptVisitor<T, S> (AstVisitor<T, S> visitor, T data)
		{
			return visitor.VisitUsingAliasDeclaration (this, data);
		}
	}
}
