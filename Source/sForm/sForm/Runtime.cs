//
// Runtime.cs
//
// Author:
//   Rasmus Pedersen (rasmus@akvaservice.dk)
//
// Copyright (C) 2009 Rasmus Pedersen
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Mono.Unix;

using sConsole;

namespace sForm
{
	public static class Runtime
	{
		#region Private Static Fields				
		#endregion

		#region Public Static Fields		
		#endregion

		#region Public Static Methods
		public static void Initialize ()
		{	
			try
			{
				SetDefaults ();			
			
				sConsole.Menu.AddItem ("addins", "sform", "sForm", "addins/sform/", 0);								
				
				// Create symlinks if they dont exist.
				if (!Directory.Exists (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/content/addins/sform"))
				{
					UnixFileInfo dirinfo = new UnixFileInfo (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sForm/data/content/addins/sform");			
					dirinfo.CreateSymbolicLink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/content/addins/sform");
				}
			
				if (!Directory.Exists (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/html/xml/addins/sform"))
				{
					UnixFileInfo dirinfo = new UnixFileInfo (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sForm/data/html/xml/addins/sform");
					dirinfo.CreateSymbolicLink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/html/xml/addins/sform");
				}
			
//				if (!File.Exists (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/html/js/sform.js"))
//				{
//					UnixFileInfo dirinfo = new UnixFileInfo (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sCMS/data/html/js/sform.js");
//					dirinfo.CreateSymbolicLink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/html/js/sform.js");
//				}				
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SFORM.INITIALIZE", exception.Message));
			}
		}
						
		private static void SetDefaults ()
		{			
		}		
		#endregion				
	}			

}
