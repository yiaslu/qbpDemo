
function Cols(width, title, titleHtml, name, align, render) {
    var col = {
        width: (width ? width + "px" : 'auto'),
        title: (title ? title : ''),
        titleHtml: titleHtml,
        name: name,
        align: (align ? align : 'left'),
        renderer: render,
        cols: []
    };
    return col;
}
function aceTable(divid) {
    var aceTable = {
        id: divid
           , width: 'auto'
        //, height: '280px'
           , cols: []
           , name: "" //当前对象的变量名
        //, url: false
           , params: {}
           , method: 'POST'
        //, cache: false
           , root: 'items'
           , items: []
        //, autoLoad: true
        //, remoteSort: false
        //, sortName: ''
        //, sortStatus: 'asc'
        //, loadingText: '正在载入...'
           , noDataText: '尚无相关数据！'
        //, loadErrorText: '数据加载出现异常'
        //, multiSelect: false
           , checkCol: false
           , indexCol: false
           , indexColWidth: 30
        //, fullWidthRows: false
           , nowrap: false
           , itemupdate: null
           , itemdelete: null
           , itemget: null
        //, showBackboard: true
        //, backboardMinHeight: 125
        //, plugins: [] //插件 插件必须实现 init($mmGrid)和params()方法，参考mmPaginator
    };


    var ttable = '<div class="row-fluid">' +
           '    <div class="span12">' +
           '        <table id="table' + aceTable.id + '" class="table table-striped table-bordered table-hover">' +
           '            <thead>' +
           '                {HEAD}' +
           '            </thead>' +
           '            <tbody>' +
           '                {ROWS}' +
           '            </tbody>' +
           '        </table>' +
           '    </div>' +
           '</div>';
    var thead = ''
           + '    <th {set}>'
           + '        <label>'
           + '          {HEAD}'
           + '        </label>'
           + '    </th>';

    var trows = '<tr>'
           + '       {TR}'
           + '</tr>';
    //初始化table；
    aceTable.initCreate = function () {
        var tableHTML = ttable.replace("{HEAD}", this._initHead());
        var rowHTML = "";
        if (this.items.length == 0) {

            rowHTML = "<tr><td colspan='" + (this.cols.length + (this.checkCol ? 1 : 0) + (this.indexCol ? 1 : 0) + (this._showExec() ? 1 : 0)) + "' style='color:red;text-align:center;line-height:80px;'>" + this.noDataText + "</td></tr>";
        }
        tableHTML = tableHTML.replace("{ROWS}", rowHTML);
        document.getElementById(this.id).innerHTML = tableHTML;


    }

    //绑定表头
    aceTable._initHead = function () {
        var heads = "<tr>";


        //显示复选框
        if (this.checkCol) {
            heads += thead.replace("{set}", "style='width:30px;text-align:center'").replace("{HEAD}", "<input type='checkbox' name='chbox_" + this.id + "' id='chbox_" + this.id + "' /><span class='lbl'></span>");
        }
        //显示序号列
        if (this.indexCol) {
            heads += thead.replace("{set}", "style='width:" + this.indexColWidth + "px;text-align:center'").replace("{HEAD}", "");
        }

        for (var i = 0; i < this.cols.length; i++) {
            var set = " style='text-align:center;width:" + this.cols[i].width + ";'";
            var titles = this.cols[i].titleHtml ? this.cols[i].titleHtml : this.cols[i].title;
            heads += thead.replace("{set}", set).replace("{HEAD}", titles);
        }

        //显示操作列
        if (this._showExec()) {
            heads += thead.replace("{set}", "style='width:80px;text-align:center'").replace("{HEAD}", "");
        }

        heads += "</tr>";
        return heads;
    }
    aceTable._showExec = function () {
        if (this.itemdelete != null || this.itemget != null || this.itemupdate != null) {
            return true;
        }
        return false;
    }
    aceTable._getExec = function (jsonstr) {
        var retHTML = "";
        var ycHTML = "";
        if (this._showExec()) {
            retHTML = "<div class='hidden-phone visible-desktop btn-group'>";
            var ycHTML = "<div class='hidden-desktop visible-phone'><div class='inline position-relative'><button class='btn btn-minier btn-yellow dropdown-toggle' data-toggle='dropdown'><i class='icon-caret-down icon-only bigger-120'></i></button><ul class='dropdown-menu dropdown-icon-only dropdown-yellow pull-right dropdown-caret dropdown-close'>";

            if (this.itemget != null) {
                retHTML += "<button class='btn btn-mini btn-' onclick='" + this.name + ".itemget(this)' ><i class='icon-zoom-in bigger-120'></i></button>";
                ycHTML += "<li><a href='#' class='tooltip-info' data-rel='tooltip' title='View' onclick='" + this.name + ".itemget(this.parentNode.parentNode.parentNode)'><span class='blue'><i class='icon-zoom-in bigger-120'></i></span></a></li>";
            }
            if (this.itemupdate != null) {
                retHTML += "<button class='btn btn-mini btn-success' onclick='" + this.name + ".itemupdate(this)'><i class='icon-edit bigger-120'></i></button>";
                ycHTML += "<li><a href='#' class='tooltip-success' data-rel='tooltip' title='Edit' onclick='" + this.name + ".itemupdate(this.parentNode.parentNode.parentNode)'><span class='green'><i class='icon-edit bigger-120'></i></span></a></li>";
            }
            if (this.itemdelete != null) {
                retHTML += "<button class='btn btn-mini btn-danger' onclick='" + this.name + ".itemdelete(this)'><i class='icon-trash bigger-120'></i></button>";
                ycHTML += "<li><a href='#' class='tooltip-error' data-rel='tooltip' title='Delete' onclick='" + this.name + ".itemdelete(this.parentNode.parentNode.parentNode)'><span class='red'><i class='icon-trash bigger-120'></i></span></a></li>";
            }

            retHTML += "</div>";
            ycHTML += "</ul></div></div>";
        }
        return (retHTML + ycHTML);
    }


    //绑定数据
    aceTable.DataBind = function () {
        if (this.items.length != 0) {
            document.getElementById("table" + this.id).getElementsByTagName('tbody')[0].innerHTML = "";
        } else {
            document.getElementById("table" + this.id).getElementsByTagName('tbody')[0].innerHTML = "<tr><td colspan='" + (this.cols.length + (this.checkCol ? 1 : 0) + (this.indexCol ? 1 : 0) + (this._showExec() ? 1 : 0)) + "' style='color:red;text-align:center;line-height:80px;'>" + this.noDataText + "</td></tr>";
            return;
        }
        var rows = "";
        for (var i = 0; i < this.items.length; i++) {
            var cols = "<tr>";
            if (this.checkCol) {
                cols += "<td style='text-align:center'> <label> <input type='checkbox' name='cbbox_" + this.id + "_" + i + "' /> <span class='lbl'></span> </label> </td>";
            }
            if (this.indexCol) {
                cols += "<td style='text-align:center;'> " + (i + 1) + " </td>";
            }
            for (var j = 0; j < this.cols.length; j++) {
                var coltext = "";
                var vals = eval('this.items[' + i + '].' + this.cols[j].name);
                if (vals != undefined)
                    coltext = vals;
                if (this.cols[j].renderer != null) {
                    coltext = this.cols[j].renderer(coltext);
                }
                if (j == 0) {
                    coltext = "<input type='hidden' value=\"" + this._itemToString(this.items[i]) + "\" />" + coltext;
                }
                cols += "<td style='text-align:" + this.cols[j].align + "'>" + coltext + "</td>";
            }

            //显示操作列
            if (this._showExec()) {
                cols += "<td>" + this._getExec() + "</td>";
            }
            cols += "</tr>";
            rows += cols;
        }
        document.getElementById("table" + this.id).getElementsByTagName('tbody')[0].innerHTML = rows;
    }
    aceTable.GetItemByBtn = function (btn) {
        var jsonstr = btn.parentNode.parentNode.parentNode.cells[0 + (this.checkCol ? 1 : 0) + (this.indexCol ? 1 : 0)].getElementsByTagName("input")[0].value;
        return eval('(' + jsonstr + ')');
    }
    aceTable._itemToString = function (o) {
        var arr = [];
        var fmt = function (s) {
            if (typeof s == 'object' && s != null) return json2str(s);
            return /^(string|number)$/.test(typeof s) ? "'" + s + "'" : s;
        }
        for (var i in o) arr.push("'" + i + "':" + fmt(o[i]));
        return '{' + arr.join(',') + '}';
    }
    return aceTable;
};