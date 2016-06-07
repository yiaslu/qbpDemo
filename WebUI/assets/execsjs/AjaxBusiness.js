
function RequestXMl() {
    var xml = "";
    var requsetobj = { "type": "json" };
    this.add = function (Key, Value) {
        requsetobj[Key] = Value;
    }
    this.getXML = function () {
        var retXml = liger.toJSON(requsetobj);
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
                    window.top.location = "/login.htm";
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