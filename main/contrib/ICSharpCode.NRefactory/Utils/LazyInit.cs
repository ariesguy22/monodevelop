﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ICSharpCode.NRefactory.TypeSystem;

namespace ICSharpCode.NRefactory.Utils
{
	public static class LazyInit
	{
		static volatile object barrier = null;
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "tmp",
		                                                 Justification = "The volatile read is important to cause a read barrier.")]
		public static void ReadBarrier()
		{
			object tmp = barrier;
		}
		
		/// <summary>
		/// Atomatically performs the following operation:
		/// - If target is null: stores newValue in target and returns newValue.
		/// - If target is not null: returns target.
		/// </summary>
		public static T GetOrSet<T>(ref T target, T newValue) where T : class
		{
			T oldValue = Interlocked.CompareExchange(ref target, newValue, null);
			return oldValue ?? newValue;
		}
	}
}