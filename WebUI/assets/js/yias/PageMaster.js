var configFileName = "license.crconfig";
var headHTML = "<link href='/assets/css/bootstrap.min.css' rel='stylesheet' type='text/css' />" +
               "<link href='/assets/css/bootstrap-responsive.min.css' rel='stylesheet' type='text/css' />" +
               "<link href='/assets/css/font-awesome.min.css' rel='stylesheet' type='text/css' />" +
               "<link href='/assets/css/ace.min.css' rel='stylesheet' type='text/css' />" +
               "<link href='/assets/css/ace-responsive.min.css' rel='stylesheet' type='text/css' />" +
               "<link href='/assets/css/ace-skins.min.css' rel='stylesheet' type='text/css' />" +
               "<script src='/assets/js/jquery-2.0.3.min.js' type='text/javascript'></script>" +
               "<script src='/assets/js/bootstrap.min.js' type='text/javascript'></script>" +
               "<script src='/assets/js/ace-elements.min.js' type='text/javascript'></script>" +
               "<script src='/assets/js/assets/js/ace.min.js' type='text/javascript'></script>" +
               "<script src='/assets/js/yias/AjaxBusiness.js' type='text/javascript'></script>";
var ListScript = "";

var EditScript = "";


function SetConfig() {
    configFileName = "license.crconfig";
}
//导入js
function GetHTMLHead() {
    document.write(headHTML);
}
function GetHTMLListHead() {
    if (window.top == window)
        location = "/Index.html";
    document.write(headHTML + ListScript);
}
function GetHTMLEditHead() {
    if (window.top == window)
        location = "/Index.html";
    document.write(headHTML + EditScript);
}
function GetHTMLLoginHead() {
    document.write(headHTML + EditScript);
}
//获取Request 传递的值
function HTTPReqeust(strParame) {
    var args = new Object();
    var query = location.search.substring(1);

    var pairs = query.split("&"); // Break at ampersand 
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('=');
        if (pos == -1) continue;
        var argname = pairs[i].substring(0, pos);
        var value = pairs[i].substring(pos + 1);
        value = decodeURIComponent(value);
        args[argname] = value;
    }
    if (args[strParame] == null)
        return "";
    return args[strParame];
}
//复制对象
function CopyModelToModel(model, tomodel) {
    for (var toKey in tomodel) {
        for (var key in model) {
            if (toKey == key && model[key] != null) {
                tomodel[toKey] = model[key];
            }
        }
    }
}

function HTMLToText(htmlStr) {
    var rethtmlStr = htmlStr;
    if (rethtmlStr && rethtmlStr != "")
        while (rethtmlStr.indexOf('"') != -1) {
            rethtmlStr = rethtmlStr.replace('"', "[-|-]");
        }
    return rethtmlStr;
}
function TextToHTML(textStr) {
    var rettextStr = textStr;
    if (rettextStr && rettextStr != "")
        while (rettextStr.indexOf("[-|-]") != -1) {
            rettextStr = rettextStr.replace("[-|-]", '"');
        }
    return rettextStr;
}