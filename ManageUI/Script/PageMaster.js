var configFileName = "license127.crconfig";
var headHTML = "<link href='/lib/ligerUI/skins/Aqua/css/ligerui-all.css' rel='stylesheet' type='text/css' />" +
               "<link href='/lib/ligerUI/skins/ligerui-icons.css' rel='stylesheet' type='text/css' />" +
               "<link href='/lib/ligerUI/skins/Gray/css/all.css' rel='stylesheet' type='text/css' />" +
               "<script src='/lib/jquery/jquery-1.3.2.min.js' type='text/javascript'></script>" +
               "<script src='/Script/AjaxBusiness.js' type='text/javascript'></script>";
var ListScript = "<script src='/lib/ligerUI/js/core/base.js' type='text/javascript'></script>" +
                "<script src='/lib/ligerUI/js/ligerui.all.js' type='text/javascript'></script>" +
                 "<script src='/lib/ligerUI/js/plugins/ligerGrid.js' type='text/javascript'></script>" +
                 " <script src='/lib/ligerUI/js/plugins/ligerToolBar.js' type='text/javascript'></script>" +
                 "<script src='/lib/ligerUI/js/plugins/ligerMenu.js' type='text/javascript'></script>";

var EditScript = "<script src='/lib/ligerUI/js/core/base.js' type='text/javascript'></script>" +
                "<script src='/lib/ligerUI/js/ligerui.all.js' type='text/javascript'></script>" +


                 "<script src='/lib/jquery-validation/jquery.validate.min.js' type='text/javascript'></script>" +
                 "<script src='/lib/jquery-validation/jquery.metadata.js' type='text/javascript'></script>" +
                 "<script src='/lib/jquery-validation/messages_cn.js' type='text/javascript'></script>";
function SetConfig() {
    configFileName = "license127.crconfig";
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