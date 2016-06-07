function Verification() {
    $.ajax({// ajax Begin
        type: 'post',
        url: '/Service.asmx/Verification',
        contentType: "application/json; charset=utf-8",
        success: function (rea) {
            if (rea.d == "UserLoginTimeOut") {
                alert("您的登陆已超时，请重新登陆！");
                window.top.location = "/login.htm";
                return;
            }
            else {
                
            
            }
        }, //success end
        error: function (result) {
            var obj = eval('(' + result.response + ')')
            alert(obj.Message);
        }
    });            //ajax end 
}