// Delay before executing asyncronis request.
_asyncdelay : 10,

new : function (title)
{
	var content = new Array ();
	content["title"] = title;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sForm.Form.New", "data", "POST", false);
	request.send (content);

	return request.respons ()["sform.form"];
},

load : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sForm.Form.Load", "data", "POST", false);	
	request.send (content);

	return request.respons ()["sform.form"];
},

save : function (form)
{					
	var content = new Array ();
	content["sform.form"] = form;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sForm.Form.Save", "data", "POST", false);		
	request.send (content);

	return true;
},		

delete : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sForm.Form.Delete", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;

	request.send (content);

	return true;
},

list : function ()
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sForm.Form.List", "data", "POST", false);		
	request.send ();

	return request.respons ()["sform.forms"];
}	