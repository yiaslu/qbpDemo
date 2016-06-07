//*****************************************************************************************
//
// 公用客户端脚本
//
//*****************************************************************************************

//是否调试
var isDebug = false;
var isCreateSubmit = true;
//页面加载事件
function OnPageLoad(){
    SetCheckboxAndRadioBorder();//去掉单选框的边框
    OnImageTempSrc();           //加载鼠标移动时图片
    try
    {
       //  CreateBgImgFun();
    }
    catch(e){
    }
    if(isCreateSubmit&&window!=window.top)
    {
        CreaetOnSubmitFun();        //动态添加From的OnSubmit事件   
    }
}
function CreaetOnSubmitFun(){
    var forms=document.forms;
    for(var i=0;i<forms.length;i++){
        forms[i].onsubmit=OnSubmitFun;
    }    
    try
    {
        window.top.DisSubmitDiv();
    }catch(e)
    {
    }
}
function OnSubmitFun(){ 
    try
    {
        window.top.ShowSubmitDiv();
    }catch(e)
    {
    }
    var forms=document.forms;
    for(var i=0;i<forms.length;i++){
        forms[i].onsubmit=ClearOnSubmitFun;
    }
}
function ClearOnSubmitFun(){
    return false;
}
//加载鼠标移动时图片
function OnImageTempSrc(){
    var inputsImage = document.getElementsByTagName("INPUT");
    for(var i=0;i<inputsImage.length;i++)
    {
        var input = inputsImage[i];
        if(input.type=="image"&&input.className=="BtnBGChange") {
           var tempSrc1=document.createAttribute("tempSrc1");
           tempSrc1.value=input.src;
           input.setAttributeNode(tempSrc1);
           var tempSrc2=document.createAttribute("tempSrc2");
           tempSrc2.value= input.src.replace("2.",".");
           input.setAttributeNode(tempSrc2);
           input.onmouseover=function(){
               if(!IsNullOrEmpty(event.srcElement.tempSrc2)){
                   event.srcElement.src=event.srcElement.tempSrc2;
               }
               this.style.cursor="hand";
           }
           input.onmouseout=function(){
               if(!IsNullOrEmpty(event.srcElement.tempSrc1)){
                   this.src=this.tempSrc1;
               }
               this.style.cursor="default";
           }
        }    
    }
    
    inputsImage = document.getElementsByTagName("img");
    for(var i=0;i<inputsImage.length;i++)
    { 
        var input = inputsImage[i];
        if(input.className=="BtnBGChange") {
            var tempSrc1=document.createAttribute("tempSrc1");
            tempSrc1.value=input.src;
            input.setAttributeNode(tempSrc1);
            var tempSrc2=document.createAttribute("tempSrc2");
            tempSrc2.value= input.src.replace("2.",".");
            input.setAttributeNode(tempSrc2);  
            input.onmouseover=function(){
                if(!IsNullOrEmpty(this.tempSrc2)){
                    this.src=this.tempSrc2;
                }
                this.style.cursor="hand";
               
            }
            input.onmouseout=function(){
                if(!IsNullOrEmpty(this.tempSrc1)){
                    this.src=this.tempSrc1;
                }
                this.style.cursor="default";
            }
        }
    }
}

function SetHome(obj,url){
  try{
    obj.style.behavior='url(#default#homepage)';
	obj.setHomePage(url);
  }
  catch(e){
    if(window.netscape) {
      try {
        netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");  
      }  
      catch (e) 
      { 
        alert("抱歉！您的浏览器不支持直接设为首页。请在浏览器地址栏输入“about:config”并回车然后将[signed.applets.codebase_principal_support]设置为“true”，点击“加入收藏”后忽略安全提示，即可设置成功。");  
      }
      var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
      prefs.setCharPref('browser.startup.homepage',url);
    }
  }
}

//去掉单选框的边框
function RemoveCheckBoxBorder(formName)
{
  var checkboxs = eval("document."+formName).getElementsByTagName("INPUT");
  for(var i=0;i<checkboxs.length;i++)
  {
    var el = checkboxs[i];
    if(el.type=="checkbox")
    {
      el.style.border = "0";
    }
  }
}

//去掉复选框和单选框的边框
function SetCheckboxAndRadioBorder() {
  var inputsCheckboxAndRadio = document.getElementsByTagName("INPUT");
  for(var i=0;i<inputsCheckboxAndRadio.length;i++)
  {
    var input = inputsCheckboxAndRadio[i];
    if(input.type=="checkbox"||input.type=="radio") {
      input.style.border = "0px";
    }    
  }
}

/**
 * 删除字符串两边空格
 */
String.prototype.trim = function(){
	return this.replace(/(^\s*)|(\s*$)/g, "");
}

/**
 * 删除标签符号
 */
String.prototype.stripTags = function(){
    return this.replace(/<\/?[^>]+>/gi, '');
}
//去掉文本框两端空格参数this
function TrimInput(src){
    src.value=src.value.trim();
}
//判断字符串是否为空
function IsNullOrEmpty(str)
{
    return str==null||str==""||str=="undefined";
}

//点击全选框按钮时执行的操作
function SelectAllCheckBoxOnClick(){
    var chk = event.srcElement;
    var inputs = document.body.getElementsByTagName("INPUT");
    for(var i=0;i<inputs.length;i++){
        var ipt = inputs[i];
        if(ipt.type=="checkbox"){
            ipt.checked = chk.checked;
        }
    }
}
//点击全选框按钮时执行的操作（不选择Disabeld为true的复选框）
function SelectAllCheckBoxByNotDisalbed(){
    var chk = event.srcElement;
    var inputs = document.body.getElementsByTagName("INPUT");
    for(var i=0;i<inputs.length;i++){
        var ipt = inputs[i];
        if(ipt.type=="checkbox"&&ipt.disabled==false){
            ipt.checked = chk.checked;
       }
    }
}

function SelectCheckBoxOnClickByCid(cid) 
{
  var chk = event.srcElement;
  var inputs = document.body.getElementsByTagName("INPUT");
  for(var i=0;i<inputs.length;i++)
  {
    var ipt = inputs[i];
    if(ipt.type=="checkbox")
    {
        if (ipt.cid!=null && ipt.cid.substr(0,ipt.cid.indexOf("_"))==cid) 
        {
            ipt.checked = chk.checked;
        }
    }
  }
}

//获得选中的项目的编号列表
function GetCheckedItemIdList(s)
{
  var list = "";
  var inputs = document.body.getElementsByTagName("INPUT");
  for(var i=0;i<inputs.length;i++){
    var ipt = inputs[i];
    if(ipt.type=="checkbox"&&ipt.checked&&ipt.value!="chkSelectAll") {
      if(list!=""){
        list += ",";
      }
      if(s==null||s==""){
        list += ipt.value;
      }
      else{
        eval("list += ipt."+s+";");
      }
    }
  }
  return list;
}

//下载附件
function DownloadFile(fjbh)
{
  location = sysPath+"/common/DownloadFile.aspx?fjbh="+fjbh;
}

//展开选项页控件
function ExpandTabControl()
{
  var oTable = document.getElementById("tabControl");
  oTable.width = "100%"; 
}

//删除指定编号的项目
function DelItem(id)
{
  if(confirm("确定删除吗？"))
  {
    location = "Delete.aspx?id="+id;
  }
}

//展开
function Expand(obj,id)
{
     if (obj.value.trim()=="展开")
     {     
        document.getElementById(id).style.display = "";
        obj.value = " 收回 ";
     }
     else
     {
        document.getElementById(id).style.display = "none";
        obj.value = " 展开 ";
     }
}

//通过链接展开
function ExpandByLink(obj,id)
{
     if (obj.innerText.trim()=="显示查询")     {     
        document.getElementById(id).style.display = "";
        obj.innerText = " 隐藏查询 ";
     }
     else
     {
        document.getElementById(id).style.display = "none";
        obj.innerText = " 显示查询 ";
     }
}


//格式化数值
function FormatNumber(number,digital) {
  return (Math.round(parseFloat(number)*Math.pow(10,digital))/Math.pow(10,digital)).toFixed(digital);
}

//获得指定的日期值
function DateValue(date) {
  date = new Date(date);
  if(date.getFullYear()==1970&&date.getMonth()==0&&date.getDate()==1) return "";
  var str = new String(date.getFullYear()),m,d;
  m = date.getMonth()+1;
  if(m<10) str += "/0" + m
  else str += "/"+m;
  d = date.getDate();
  if(d<10) str += "/0" + d
  else str += "/"+d;
  return str;
}

//获得时间值
function TimeValue(d) {
  d = new Date(d);
  if(d.getFullYear()==1970&&d.getMonth()==0&&d.getDate()==1) return "";
  var ret = "";
  if(d.getHours()>9) ret += d.getHours()
  else ret += "0" + d.getHours();
  if(d.getMinutes()>9) ret += ":" + d.getMinutes()
  else ret += ":0" + d.getMinutes();
  if(d.getSeconds()>9) ret += ":" + d.getSeconds()
  else ret += ":0" + d.getSeconds();
  return ret;
}

function DateTime(d) {
  return DateValue(d) + " " + TimeValue(d);
}

//去掉字符串两端的空格
function Trim(str) {
  var i,start=-1,end,s = new String(str);
  for(i=0;i<s.length;i++) {
    if(s.charAt(i)!=' ') {
      if(start==-1) start = i;
      end = i;  
    }
  }
  return s.substring(start,end+1);
}

//返回字符串的长度：字母和数字算一位，汉字算两位
function Length(s) {
  var str = new String(s);
  var i,length = 0;
  for(i=0;i<str.length;i++) {
    if(str.charCodeAt(i)>255) length += 2
    else length ++;
  }
  return length;
}

//判断指定的参数是否为空
function IsNull(arg)
{

  return (arg==""||(new String(arg))=="undefined"||arg==null||arg==undefined);
}

function IsInfinity(arg) 
{
    return (arg=="Infinity")
}
//
function CheckNull(obj,msg)
{
  if(IsNull(obj.value))
  { 
    Msg(obj,"请输入"+msg+"！");
    return true;
  }
  return false;
}

//判断是否是整数
function IsInt(arg) {
  return !(this.IsNull(arg)||isNaN(arg)||((new String(arg)).indexOf(".")!=-1));
}

//判断是否是浮点数
function IsFloat(arg){
  return !(isNaN(arg)||isNaN(parseFloat(arg)));
}



function CheckInt(arg,msg)
{
  if(!IsInt(arg)) Msg(this,"请输入正确的"+msg+"！");
}

//判断是否是数值
function IsNumber(arg) {
  return !(this.IsNull(arg)||isNaN(arg));
}

function CheckNumber(arg,msg)
{
  if(!IsNumber(arg)) Msg(this,"请输入正确的"+msg+"！");
}

//判断是否是正确的日期值
function IsDate(argYear,argMonth,argDay)
{
  var myYear = new String(argYear);
  var myMonth = new String(argMonth);
  var myDate = new String(argDay);
  var ret = (IsInt(argYear)&&IsInt(argMonth)&&IsInt(argDay));

  if(ret)
  {
    var theDate = new Date(myYear,parseInt(myMonth)-1,myDate);
    if(theDate.getFullYear()!=parseInt(myYear)
      ||theDate.getMonth()!=(parseInt(myMonth)-1)
      ||theDate.getDate()!=parseInt(myDate))
      ret = false;
  }

  return ret;
}
//转换成日期格式
//str格式必须为0000-00-00
function ParseDate(str)
{
  var dt=new Date();
  var myYear = parseInt(str.substr(0,str.indexOf("-")))+"";
  var m = str.substr(0,str.lastIndexOf("-"));
  var myMonth=m.substr(m.lastIndexOf("-")+1,m.length);
  if(myMonth.indexOf('0')==0)
  {
     myMonth=myMonth.substring(1,2);
  }
  var myDate = str.substr(str.lastIndexOf("-")+1,str.length)+"";
  if(myDate.indexOf('0')==0)
  {
     myDate=myDate.substring(1,2);
  }
  
  return new Date(myYear,parseInt(myMonth)-1,myDate);
}

function CheckDate(argYear,argMonth,argDay,msg)
{
  if(!IsDate(argYear,argMonth,argDay)) Msg(this,msg+"不是有效的日期！"); 
}

//判断是否大于指定的值
function IsOverNumber(arg,ref)
{
  return (parseFloat(arg)>parseFloat(ref));
}

function CheckOverNumber(arg,ref,msg)
{
  if(IsOverNumber(arg,ref)) Msg(this,msg+"不能大于"+ref+"！");
}

//判断是否小于指定的值
function IsLessNumber(arg,ref)
{
  return (parseFloat(arg)<parseFloat(ref));
}

function CheckLessNumber(arg,ref,msg)
{
  if(IsLessNumber(arg,ref)) Msg(this,msg+"不能小于"+ref+"！");
}

//判断是否超过指定的长度
function IsOverLength(arg,len)
{
  return (Length(arg)>len);
}

function CheckOverLength(obj,len,msg)
{
  if(IsOverLength(obj.value,len)) 
  {
    Msg(obj,msg+"不能超过"+len+"个字符！");
    return true;
  }
  return false;
}

//判断是否少于指定的长度
function IsLessLength(arg,len)
{
  return (Length(arg)<len);
}

function CheckLessLength(arg,len,msg)
{
  if(IsLessLength(arg,len)) Msg(this,msg+"不能少于"+len+"个字符！");
}

//判断是否等于指定的长度
function IsEqualLength(arg,len)
{
  return (Length(arg)==len);
}

function CheckEqualLength(arg,len,msg)
{
  if(!IsEqualLength(arg,len)) Msg(this,msg+"必须是"+len+"个字符！");
}

function Now()
{
  return DateTime(new Date());
}

//货币输入框失去焦点时执行
function MoneyInputOnBlur(input) {
  if(!IsNumber(input.value)) {
    input.value = "0.00";
    input.focus();
  } else {
    input.value = FormatNumber(input.value,2);
  }
}

//计算日期
function CalculatedDate(d,type,count)
{
    var arys = new Array();
    arys = d.split('-');
    date = new Date(arys[0],arys[1]-1,arys[2]);
    if (type=="Month")
    {
        date.setMonth(date.getMonth()+count);
    }else if (type=="Date")
    {
        date.setDate(date.getDate()+count);
    }else if (type=="Year")
    {
        date.setFullYear(date.getFullYear()+count);
    }
    
    return date.getFullYear()+"-"+(date.getMonth()+1)+"-"+date.getDate();
}
//缩略图
function DrawImage(ImgD,width_s,height_s)
        {
            /*var width_s=139;
            var height_s=104;
            */
            var image=new Image();
            image.src=ImgD.src;
            if(image.width>0 && image.height>0)
            {
                flag=true;
                if(image.width/image.height>=width_s/height_s)
                {
                    if(image.width>width_s)
                    {
                        ImgD.width=width_s;
                        ImgD.height=(image.height*width_s)/image.width;
                    }
                    else
                    {
                        ImgD.width=image.width;
                        ImgD.height=image.height;
                    }
                }
                else
                {
                    if(image.height>height_s)
                    {
                        ImgD.height=height_s;
                        ImgD.width=(image.width*height_s)/image.height;
                    }
                    else
                    {
                        ImgD.width=image.width;
                        ImgD.height=image.height;
                    }
                }
            }
            /*else
            {
                ImgD.src="";
                ImgD.alt=""
            }*/
        }


function TrOnMouseOver(trs)
{
    var yclassName=trs.className;
    trs.className="RowMouseOverStyle";
    trs.onmouseout=function()
    {
        trs.className=yclassName;
    }
}


function chkUsername(username) {
	if(username=="") {
		return 0;
	}
	else if( /^\d.*$/.test( username ) ){
		//用户名不能以数字开头
		return -1;
	}
	else if(fLen( username )<6 || fLen( username )>18 ){
		//合法长度为6-18个字符
		return -2;
	}
	else if(! /^\w+$/.test( username ) ){
		//用户名只能包含_,英文字母，数字
		return -3;
	}
	else if(! /^([a-z]|[A-Z])[0-9a-zA-Z_]+$/.test( username ) ){
		//用户名只能英文字母开头
		return -4;
	}
	else if(!(/[0-9a-zA-Z]+$/.test( username ))){
		//用户名只能英文字母或数字结尾
		return -5;
	}
	return 1;
}
//改变父级地址
function ToParentUrl(url){  
    try{
        if(isCreateSubmit&&window!=window.top){
            window.top.ShowSubmitDiv();
        }
    }
    catch(exc){
    }
    finally{
        parent.location.href=url;
    }
    return false;
}
//改变当前地址
function ToThisUrl(url){
    try{
        if(isCreateSubmit&&window!=window.top){
            window.top.ShowSubmitDiv();
        }
    }
    catch(exc){
    }
    finally{
        location.href=url;
    }
    return false;
}
//改变框架地址
function ToFramesUrl(leftUrl,rightUrl){
    try{
        if(isCreateSubmit&&window!=window.top){
            window.top.ShowSubmitDiv();
        }
    }
    catch(exc){
    }
    finally{
        parent.frames.frameLeft.location=leftUrl;
        parent.frames.frameRight.location=rightUrl;
    }
    return false;
}

//格式化钱类型-------------------------------
function outputMoney(number) 
{ 
    number=number.replace(/\,/g,"");  //替换    /g全局匹配
    if (number=="")
    {
        return ""; 
    }
    if(number<0) 
    {
        return '-'+outputDollars(Math.floor(Math.abs(number)-0) + '') + outputCents(Math.abs(number) - 0); 
    }
    else   
    {        //Math.abs 返回数的绝对值,Math.floor返回任意浮点数的整数部分
        return outputDollars(Math.floor(number-0) + '') + outputCents(number - 0); 
    }
} 

function outputDollars(number) 
{ 
    var isFs="";
    if(parseFloat(number)<0)
    {
        isFs="-";
        number=(number*-1)+"";
    }
    if (number.length<= 3) 
    {
        return (number == '' ? '0' : isFs+number); 
    }
    else 
    { 
        var mod = number.length%3;     //值为3的余数
        var output = (mod == 0 ? '' : (number.substring(0,mod))); 
        for (i=0 ; i< Math.floor(number.length/3) ; i++) 
        { 
            if ((mod ==0) && (i ==0)) 
            {
                output+= number.substring(mod+3*i,mod+3*i+3); 
            }
            else 
            {
                output+= ',' + number.substring(mod+3*i,mod+3*i+3); 
            }
        } 
        return (isFs+output); 
    } 
} 
function outputCents(amount) 
{ 
amount = Math.round( ( (amount) - Math.floor(amount) ) *100);    //Math.round四舍五入
return (amount<10 ? '.0' + amount : '.' + amount); 
}
/*清除Tree A标签*/
function ClearTagA()
{
      var ahs=document.all.tags("A");
      for(var i=0;i<ahs.length;i++)
      {
          if(ahs[i].getElementsByTagName("img").length==0)
          {
              ahs[i].href="javascript:void(0)";
          }
      }
 }
 
 
 function topage(url) 
 {
    location = url;
    return false;
 }
 
 function $(id)
 {
    return document.getElementById(id);
 }
 
 function filtersql(sql) 
 {
    return sql.toLowerCase().replace("insert","").replace("delete","").replace("update","").replace("drop","").replace("create","").replace("alter","").replace("exec","");
 }