function Login(nameinput, passinput) {
    var name = document.getElementById(nameinput).value;
    var pass = document.getElementById(passinput).value;
    if (!name) {
        alert("请录入用户名！");
        return;
    }
    if (!pass) {
        alert("请录入密码！");
        return;
    }
    var request = new DBRequest("SystemBLL.UserLogin", "Login", "SystemBLL");
    request.add("userName", name);
    request.add("userPass", pass);
    request.send(function (ret) {
        if (ret.d) {
            location = "index.html";
        } else {
            alert("登陆账号或密码错误，请重新登陆！");
            return;
        }
    });
}