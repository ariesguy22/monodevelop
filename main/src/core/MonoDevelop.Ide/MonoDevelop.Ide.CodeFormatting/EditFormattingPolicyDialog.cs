// 
// EditFormattingPolicyDialog.cs
//  
// Author:
//       Mike Krüger <mkrueger@novell.com>
// 
// Copyright (c) 2009 Mike Krüger
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

using System;
using MonoDevelop.Core;
using MonoDevelop.Projects.Text;

namespace MonoDevelop.Ide.CodeFormatting
{
	public partial class EditFormattingPolicyDialog : Gtk.Dialog
	{
		CodeFormatSettings    settings;
		CodeFormatDescription description;
		
		public EditFormattingPolicyDialog()
		{
			this.Build();
			buttonOk.Clicked += delegate {
				if (description == null)
					return;
				settings.Name = this.entryName.Text;
			};
		}
		
		public void SetFormat (CodeFormatDescription description, CodeFormatSettings settings)
		{
			this.description = description;
			this.settings    = settings;
			this.entryName.Text = settings.Name;
			while (notebookCategories.NPages > 0) {
				notebookCategories.RemovePage (0);
			}
			if (description != null) {
				foreach (CodeFormatCategory category in description.SubCategories) {
					AddCategoryPage (category);
				}
			}
			notebookCategories.ShowAll ();
		}
		
		void AddCategoryPage (CodeFormatCategory category)
		{
			Gtk.Label label = new Gtk.Label (GettextCatalog.GetString (category.DisplayName));
			Gtk.TreeView tree = new Gtk.TreeView ();
			notebookCategories.AppendPage (tree, label);
		}
	}
}
