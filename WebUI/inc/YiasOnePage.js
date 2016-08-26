
function ObjPageSet(lm) {
    var _opset = {
        tableid: "",
        tabledivid: "",
        buttonid: "headbuttons",
        tTable: null,
        ofrom: null,
        olm: lm,
        state: "insert",
        cols: {}
    };
    _opset.LoadPage = function () {
        var request = new DBRequest("PageBLL.DataSet", "GetTableColumn", "PageBLL");
        request.add("tableid", this.tableid);
        request.send(function (ret) {
            _opset.cols = ret.d;
            _opset.createTable();
            _opset.createFrom();
            _opset.BindTable();
        });
    }
    _opset._orderItem = function (itemName) {
        return this.cols.sort(getSortFun("asc", itemName));
    }
    _opset.createTable = function () {
        this.tTable = new aceTable(this.tabledivid);
        var colsList = this._orderItem("listorder");
        for (var i = 0; i < colsList.length; i++) {
            if (colsList[i].listshow) {
                if (contType[this.cols[i].control] == 8) {
                    this.tTable.cols.push(new Cols(null, colsList[i].colname, '', colsList[i].colcode, 'center', function (t) {
                        if (t)
                            return FormatDate(t);
                        return "";
                    }));
                } else
                    this.tTable.cols.push(new Cols(null, colsList[i].colname, '', colsList[i].colcode, '', null));
            }
        }
        this.tTable.name = this.olm + ".tTable";

        this.tTable.itemupdate = function (btn) {
            var item = _opset.tTable.GetItemByBtn(btn);
            _opset.ofrom.SetControlValue(item);
            _opset.state = item.tid;
            $("#my-modal").modal("show");
        }

        this.tTable.itemdelete = function (btn) {
            if (confirm("是否确定删除？")) {
                var item = _opset.tTable.GetItemByBtn(btn);
                var request = new DBRequest("PageBLL.DataSet", "DeleteTable", "PageBLL");
                request.add("tableid", _opset.tableid);
                request.add("tid", item.tid);
                request.send(function () {
                    _opset.BindTable();
                })
            }
        }
        this.tTable.initCreate();

    }
    _opset.BindTable = function () {
        var request = new DBRequest("PageBLL.DataSet", "GetBindTable", "PageBLL");
        request.add("tableid", this.tableid);
        request.send(function (ret) {
            if (ret.d != null) {
                _opset.tTable.items.length = 0;
                for (var i = 0; i < ret.d.length; i++) {
                    _opset.tTable.items.push(ret.d[i]);
                }
                _opset.tTable.DataBind();
            }
        });
    }
    _opset.createFrom = function () {
        this.ofrom = new aceFrom("from");
        var colsList = this._orderItem("order");
        for (var i = 0; i < colsList.length; i++) {
            if (colsList[i].show)
                this.ofrom.cols.push(new controls(colsList[i].colcode, colsList[i].colid, colsList[i].colname, contType[colsList[i].control], null, colsList[i].coldefault));
        }
        this.ofrom.initCreate();
    }
    _opset.Insert = function () {
        this.state = "insert";
        $("#my-modal").modal("show");
    }
    _opset.Save = function () {
        var info = this.ofrom.GetControlValue();
        var requset = null;
        if (this.state == "insert") {
            request = new DBRequest("PageBLL.DataSet", "InsertTable", "PageBLL");
        } else {
            info.tid = this.state;
            request = new DBRequest("PageBLL.DataSet", "UpdateTable", "PageBLL");
        }
        request.add("tableid", this.tableid);
        request.add("jsoninfo", info);
        request.send(function () {
            _opset.ofrom.CleanControl();
            $("#my-modal").modal("hide");
            _opset.BindTable();
        })
    }
    return _opset;
}
