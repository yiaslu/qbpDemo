var configFileName = "license.crconfig";
function RequestXMl() {
    var xml = "";
    var requsetobj = { "type": "json" };
    this.add = function (Key, Value) {
        requsetobj[Key] = Value;
    }
    this.getXML = function () {
        var retXml = toJSON(requsetobj);
        while (retXml.indexOf("'") != -1) {
            retXml = retXml.replace("'", "[+|+]");
        }
        return retXml;
    }
    this.get = function (Key) {
        return requsetobj[Key];
    }
}

//获得节点文本
function GetNodeText(node) {
    if (window.ActiveXObject) return node.text
    else return node.textContent;
}

//设置节点文本
function SetNodeText(node, text) {
    if (text == null) return;
    if (window.ActiveXObject) node.text = text
    else node.textContent = text;
}
function DBSendRequest(typeName, Method, BusinessName, types) {
    var root = new RequestXMl();

    root.add("typeName", typeName);
    root.add("Method", Method);
    root.add("BusinessName", BusinessName);
    root.add("types", types);
    root.add("configFileName", configFileName);
    this.add = function (Key, Value) {
        root.add(Key, Value);
    }
    //发送请求
    this.send = function (DBResponse) {
        if (root.get("typeName") == null || root.get("typeName") == "") {
            alert("请添加实例类名称！");
            return;
        }
        if (types == "Method")
            if (root.get("Method") == null || root.get("Method") == "") {
                alert("请添加实例方法名称！");
                return;
            }

        $.ajax({// ajax Begin
            type: 'post',
            data: "{'roots':'" + root.getXML() + "'}",
            url: '/Service.asmx/Send',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (rea) {
                if (rea.d == "UserLoginTimeOut") {
                    alert("您的登陆已超时，请重新登陆！");
                    window.top.location = "/login.html";
                    return;
                }
                var func = eval(DBResponse);
                new func(rea);
            }, //success end
            error: function (result) {
                var obj = null;
                if (result.response) {
                    obj = eval('(' + result.response + ')')
                } else {
                    obj = eval('(' + result.responseText + ')')
                }
                if (obj.Message == "UserLoginTimeOut") {
                    alert("您的登陆已超时，请重新登陆！");
                    window.top.location = "/login.html";
                } else
                    alert(obj.Message);
            }
        });           //ajax end 
    }
    return this;
}
function Models(typeName, BusinessName) {
    var req = DBSendRequest(typeName, "", BusinessName, "Class");
    this.send = function (DbResponse) {
        req.send(DbResponse);
    }
}
function DBRequest(typeName, Method, BusinessName) {
    var req = DBSendRequest(typeName, Method, BusinessName, "Method");
    this.send = function (DbResponse) {
        req.send(DbResponse);
    }
    this.add = function (Key, Value) {
        req.add(Key, Value);
    }
}

function toJSON(o) {
    var f = function (n) {
        return n < 10 ? '0' + n : n;
    },
    meta = {    // table of character substitutions
        '\b': '\\b',
        '\t': '\\t',
        '\n': '\\n',
        '\f': '\\f',
        '\r': '\\r',
        '"': '\\"',
        '\\': '\\\\'
    },
    escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
    quote = function (value) {
        escapable.lastIndex = 0;
        return escapable.test(value) ?
            '"' + value.replace(escapable, function (a) {
                var c = meta[a];
                return typeof c === 'string' ? c :
                    '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
            }) + '"' :
            '"' + value + '"';
    };
    if (o === null) return 'null';
    var type = typeof o;
    if (type === 'undefined') return undefined;
    if (type === 'string') return quote(o);
    if (type === 'number' || type === 'boolean') return '' + o;
    if (type === 'object') {
        if (o.constructor === Date) {
            return isFinite(o.valueOf()) ?
               '"' + o.getFullYear() + '-' +
             f(o.getMonth() + 1) + '-' +
             f(o.getDate()) + ' ' +
             f(o.getHours()) + ':' +
             f(o.getMinutes()) + ':' +
             f(o.getSeconds()) + '"' : null;
        }
        if (typeof o.toJSON === 'function') {
            return toJSON(o.toJSON());
        }
        var pairs = [];
        if (o.constructor === Array) {
            for (var i = 0, l = o.length; i < l; i++) {
                pairs.push(toJSON(o[i]) || 'null');
            }
            return '[' + pairs.join(',') + ']';
        }
        var name, val;
        for (var k in o) {
            type = typeof k;
            if (type === 'number') {
                name = '"' + k + '"';
            } else if (type === 'string') {
                name = quote(k);
            } else {
                continue;
            }
            type = typeof o[k];
            if (type === 'function' || type === 'undefined') {
                continue;
            }
            val = toJSON(o[k]);
            pairs.push(name + ':' + val);
        }
        return '{' + pairs.join(',') + '}';
    }
};