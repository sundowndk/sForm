//
// Form.cs
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
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using SNDK;

namespace sForm
{
	public class Form
	{
		#region Private Static Fields
		private static Regex ExpMatchInserts = new Regex (@"\%\%(.+?)\%\%", RegexOptions.Compiled);
		#endregion

		#region Public Static Fields
		public static string DatastoreAisle = "autoform_forms";
		#endregion

		#region Private Fields
		private Guid _id;

		private int _createtimestamp;
		private int _updatetimestamp;

		private string _title;
		private string _emailfrom;
		private string _emailto;
		private string _emailsubject;
		private string _emailbody;
		private Enums.EmailBodyType _emailbodytype;
		#endregion

		#region Public Fields
		public Guid Id
		{
			get
			{
				return this._id;
			}
		}

		public int CreateTimestamp
		{
			get
			{
				return this._createtimestamp;
			}
		}

		public int UpdateTimestamp
		{
			get
			{
				return this._updatetimestamp;
			}
		}

		public string Title
		{
			get
			{
				return this._title;
			}

			set
			{
				this._title = value;
			}
		}

		public string EmailFrom
		{
			get
			{
				return this._emailfrom;
			}

			set
			{
				this._emailfrom = value;
			}
		}

		public string EmailTo
		{
			get
			{
				return this._emailto;
			}

			set
			{
				this._emailto = value;
			}
		}

		public string EmailSubject
		{
			get
			{
				return this._emailsubject;
			}

			set
			{
				this._emailsubject = value;
			}
		}

		public string EmailBody
		{
			get
			{
				return this._emailbody;
			}

			set
			{
				this._emailbody = value;
			}
		}

		public Enums.EmailBodyType EmailBodyType
		{
			get
			{
				return this._emailbodytype;
			}

			set
			{
				this._emailbodytype = value;
			}
		}
		#endregion

		#region Constructor
		public Form ()
		{
			this._id = Guid.NewGuid ();
			this._createtimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._emailfrom = string.Empty;
			this._emailto = string.Empty;
			this._emailsubject = string.Empty;
			this._emailbody = string.Empty;
			this._emailbodytype = Enums.EmailBodyType.Plain;
		}
		#endregion

		#region Public Methods
		public void Save ()
		{
			try
			{
				this._updatetimestamp = Date.CurrentDateTimeToTimestamp ();
				
				Hashtable item = new Hashtable ();

				item.Add ("id", this._id);
				item.Add ("createtimestamp", this._createtimestamp);
				item.Add ("updatetimestamp", this._updatetimestamp);
				item.Add ("emailfrom", this._emailfrom);
				item.Add ("emailto", this._emailto);
				item.Add ("emailsubject", this._emailsubject);
				item.Add ("emailbody", this._emailbody);
				item.Add ("emailbodytype", this._emailbodytype);

				SorentoLib.Services.Datastore.Set (DatastoreAisle, this._id.ToString (), SNDK.Convert.ToXmlDocument (item, this.GetType ().FullName.ToLower ()));
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SFORM.FORM", exception.Message));

				// EXCEPTION: Exception.FormSave
				throw new Exception (string.Format (Strings.Exception.FormSave, this._id.ToString ()));
			}			
		}

		public void Send (SorentoLib.Session Session)
		{
			string emailfrom = this._emailfrom;
			string emailto = this._emailto;
			string emailsubject = this._emailsubject;
			string emailbody = this._emailbody;

			#region FROM
			{
				MatchCollection tokens = ExpMatchInserts.Matches (emailfrom);
				if (tokens.Count > 0)
				{
					for (int token = 0; token < tokens.Count; token++)
					{
						try
						{
							string key = tokens[token].Groups[1].Value;
							string value = Session.Request.QueryJar.Get (key).Value;
							emailfrom = emailfrom.Replace ("%%" + key + "%%", value);
						}
						catch
						{}
					}
				}
			}
			#endregion

			#region TO
			{
				MatchCollection tokens = ExpMatchInserts.Matches (emailto);
				if (tokens.Count > 0)
				{
					for (int token = 0; token < tokens.Count; token++)
					{
						try
						{
							string key = tokens[token].Groups[1].Value;
							string value = Session.Request.QueryJar.Get (key).Value;
							emailto = emailto.Replace ("%%" + key + "%%", value);
						}
						catch
						{}
					}
				}
			}
			#endregion

			#region SUBJECT
			{
				MatchCollection tokens = ExpMatchInserts.Matches (emailsubject);
				if (tokens.Count > 0)
				{
					for (int token = 0; token < tokens.Count; token++)
					{
						try
						{
							string key = tokens[token].Groups[1].Value;
							string value = Session.Request.QueryJar.Get (key).Value;
							emailsubject = emailsubject.Replace ("%%" + key + "%%", value);
						}
						catch
						{}
					}
				}
			}
			#endregion

			#region BODY
			{
				MatchCollection tokens = ExpMatchInserts.Matches (emailbody);
				if (tokens.Count > 0)
				{
					for (int token = 0; token < tokens.Count; token++)
					{
						try
						{
							string key = tokens[token].Groups[1].Value;
							string value = Session.Request.QueryJar.Get (key).Value;
							emailbody = emailbody.Replace ("%%" + key + "%%", value);
						}
						catch
						{}
					}
				}
			}
			#endregion

			if (this._emailbodytype == sForm.Enums.EmailBodyType.HTML)
			{
				SorentoLib.Tools.Helpers.SendMail (emailfrom, emailto, emailsubject, emailbody, true);
			}
			else
			{
				SorentoLib.Tools.Helpers.SendMail (emailfrom, emailto, emailsubject, emailbody);
			}
		}

//		public void ToAjaxRespons (SorentoLib.Ajax.Respons Respons)
//		{
//			Respons.Data = ToAjaxItem ();
//		}
//
//		public Hashtable ToAjaxItem ()
//		{
//			Hashtable result = new Hashtable ();
//
//			result.Add ("id", this._id);
//			result.Add ("createtimestamp", this._createtimestamp);
//			result.Add ("updatetimestamp", this._updatetimestamp);
//			result.Add ("title", this._title);
//			result.Add ("emailto", this._emailto);
//			result.Add ("emailfrom", this._emailfrom);
//			result.Add ("emailsubject", this._emailsubject);
//			result.Add ("emailbody", this._emailbody);
//			result.Add ("emailbodytype", this._emailbodytype.ToString ().ToLower ());
//
//			return result;
//		}
//		
		public XmlDocument ToXmlDocument ()
		{
			Hashtable result = new Hashtable ();

			result.Add ("id", this._id);
			result.Add ("createtimestamp", this._createtimestamp);
			result.Add ("updatetimestamp", this._updatetimestamp);
			result.Add ("title", this._type);
			result.Add ("emailto", this._name);
			result.Add ("emailfrom", this._status);
			result.Add ("emailsubject", this._emailsubject);
			result.Add ("emailbody", this._emailbody);
			result.Add ("emailbodytype", this._emailbodytype);

			return SNDK.Convert.ToXmlDocument (result, this.GetType ().FullName.ToLower ());
		}		
		#endregion

		#region Public Static Methods
		public static Form Load (Guid Id)
		{
			try
			{
				Hashtable item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (Services.Datastore.Get<XmlDocument> (DatastoreAisle, id.ToString ()).SelectSingleNode ("(//sorentolib.usergroup)[1]")));
				result = new Usergroup ();

				result._id = new Guid ((string)item["id"]);

				if (item.ContainsKey ("createtimestamp"))
				{
					result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
				}

				if (item.ContainsKey ("updatetimestamp"))
				{
					result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
				}
				
				if (item.ContainsKey ("title"))
				{
					result._title = (string)item["title"];
				}
				
				if (item.ContainsKey ("emailto"))
				{
					result._emailto = (string)item["emailto"];
				}
				
				if (item.ContainsKey ("emailfrom"))
				{
					result._emailfrom = (string)item["emailfrom"];
				}
				
				if (item.ContainsKey ("emailsubject"))
				{
					result._emailsubject = (string)item["emailsubject"];
				}
				
				if (item.ContainsKey ("emailbody"))
				{
					result._emailbody = (string)item["emailbody"];
				}
				
				if (item.ContainsKey ("emailbodytype"))
				{
					result._emailbodytype = SNDK.Convert.StringToEnum<Enums.EmailBodyType> ((string)item["emailbodytype"]);
				}
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				Services.Logging.LogDebug (string.Format (Strings.LogDebug.ExceptionUnknown, "SFORM.FORM", exception.Message));

				// EXCEPTION: Excpetion.FormLoad
				throw new Exception (string.Format (Strings.Exception.FormLoad, Id.ToString ()));
			}
		}

		public static void Delete (Guid Id)
		{
			try
			{
				Services.Datastore.Delete (DatastoreAisle, id.ToString ());

				ServiceStatsUpdate ();
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				Services.Logging.LogDebug (string.Format (Strings.LogDebug.ExceptionUnknown, "SFORM.FORM", exception.Message));

				// EXCEPTION: Exception.FormDelete
				throw new Exception (string.Format (Strings.Exception.FormDelete, Id.ToString ()));
			}
		}

		public static List<Form> List ()
		{
			List<Form> result = new List<Form> ();

			foreach (string id in SorentoLib.Services.Datastore.ListOfShelfs (DatastoreAisle))
			{
				try
				{
					result.Add (Form.Load (new Guid (id)));
				}
				catch (Exception exception)
				{
					// LOG: LogDebug.ExceptionUnknown
					Services.Logging.LogDebug (string.Format (Strings.LogDebug.ExceptionUnknown, "SFORM.FORM", exception.Message));
					
					SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.FormList, id));
				}
			}

			return result;
		}

//		public static Form FromAjaxRequest (SorentoLib.Ajax.Request Request)
//		{
//			return FromAjaxItem (Request.Data);
//		}
//
//		public static Form FromAjaxItem (Hashtable Item)
//		{
//			Form result;
//
//			Guid id = Guid.Empty;
//
//			try
//			{
//				id = new Guid ((string)Item["id"]);
//			}
//			catch {}
//
//			if (id != Guid.Empty)
//			{
//				try
//				{
//					result = Form.Load (id);
//				}
//				catch
//				{
//					result = new Form ();
//					result._id = id;
//					if (Item.ContainsKey ("createtimestamp"))
//					{
//						result._createtimestamp = int.Parse ((string)Item["createtimestamp"]);
//					}
//
//					if (Item.ContainsKey ("updatetimestamp"))
//					{
//						result._createtimestamp = int.Parse ((string)Item["updatetimestamp"]);
//					}
//				}
//			}
//			else
//			{
//				result = new Form ();
//			}
//
//			if (Item.ContainsKey ("title"))
//			{
//				result._title = (string)Item["title"];
//			}
//
//			if (Item.ContainsKey ("emailto"))
//			{
//				result._emailto = (string)Item["emailto"];
//			}
//
//			if (Item.ContainsKey ("emailfrom"))
//			{
//				result._emailfrom = (string)Item["emailfrom"];
//			}
//
//			if (Item.ContainsKey ("emailsubject"))
//			{
//				result._emailsubject = (string)Item["emailsubject"];
//			}
//
//			if (Item.ContainsKey ("emailbody"))
//			{
//				result._emailbody = (string)Item["emailbody"];
//			}
//
//			if (Item.ContainsKey ("emailbodytype"))
//			{
//				try
//				{
//					result._emailbodytype = SNDK.Convert.StringToEnum<Enums.EmailBodyType> ((string)Item["emailbodytype"]);
//				}
//				catch
//				{
//					throw new Exception (string.Format (Strings.Exception.FormAjaxItem, "EMAILBODYTYPE"));
//				}
//			}
//
//			return result;
//		}
		
		public static Form FromXmlDocument (XmlDocument xmlDocument)
		{
			Hashtable item;
			Usergroup result;

			try
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (xmlDocument.SelectSingleNode ("(//sform.form)[1]")));
			}
			catch
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (xmlDocument);
			}

			if (item.ContainsKey ("id"))
			{
				try
				{
					result = Load (new Guid ((string)item["id"]));
				}
				catch
				{
					result = new Form ();
					result._id = new Guid ((string)item["id"]);
				}
			}
			else
			{
				// EXCEPTION: Exception.FormFromXMLDocument
				throw new Exception (Strings.Exception.FormFromXMLDocument);
			}
			
			if (item.ContainsKey ("createtimestamp"))
			{
				result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
			}

			if (item.ContainsKey ("updatetimestamp"))
			{
				result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
			}
				
			if (item.ContainsKey ("title"))
			{
				result._title = (string)item["title"];
			}
				
			if (item.ContainsKey ("emailto"))
			{
				result._emailto = (string)item["emailto"];
			}
				
			if (item.ContainsKey ("emailfrom"))
			{
				result._emailfrom = (string)item["emailfrom"];
			}
				
			if (item.ContainsKey ("emailsubject"))
			{
				result._emailsubject = (string)item["emailsubject"];
			}
				
			if (item.ContainsKey ("emailbody"))
			{
				result._emailbody = (string)item["emailbody"];
			}
				
			if (item.ContainsKey ("emailbodytype"))
			{
				result._emailbodytype = SNDK.Convert.StringToEnum<Enums.EmailBodyType> ((string)item["emailbodytype"]);
			}			

			return result;
		}		
		#endregion
	}
}
