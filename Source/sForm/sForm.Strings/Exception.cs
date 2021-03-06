//
// Exception.cs
//  
// Author:
//       Rasmus Pedersen <rasmus@akvaservice.dk>
// 
// Copyright (c) 2010 Rasmus Pedersen
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

namespace sForm.Strings
{
	public class Exception
	{
		#region FORM
		public static string FormLoad = "[sForm.Form]: FORM with id: {0} was not found.";
		public static string FormSave = "[sForm.Form]: Could not save FORM with id: {0}";
		public static string FormDelete = "[sForm.Form]: Could not delete FORM with id: {0}";
		public static string FormAjaxItem = "[sForm.Form]: Could not recreate FORM from ajaxitem. Error occured with: {0}";
		public static string FormFromXMLDocument = "[sForm.Form]: Could not create FORM from xmldocument.";
		#endregion

		#region RESOLVER
		public static string ResolverMethodNotFound = "[sForm]: Method {0} was not found.";
		public static string ResolverSessionPriviliges = "[sForm]: Current session does not have priviliges to access method '{0}'";
		#endregion

		#region FUNCTION
		public static string FunctionMethodNotFound = "[sForm]: Method '{0}' was not found.";
		#endregion

		#region AJAX
		public static string AjaxMethodNotFound = "[sForm]: Method '{0}' was not found.";
		public static string AjaxSessionPriviliges = "[sForm]: Current session does not have priviliges to access method '{0}'";
		#endregion
	}
}

