function flowNodeList() {
    var flowNodeData = [];
    flowNodeData._getflowNode = function (id) {
        var flowNode = null;
        if (flowPageNodeList) {
            for (var i = 0; i < flowPageNodeList.length; i++) {
                if (flowPageNodeList[i].Flowid == id) {
                    flowNode = flowPageNodeList[i];
                }
            }
        }
        if (flowNode == null) {
            flowNode = flowNodeData._setflowNode(id);
        }
        return flowNode;
    }
    flowNodeData._setflowNode = function (id) {
        var item = ItemNode(id);
        flowPageNodeList.push(item);
        return item;
    }
    flowNodeData._updateflowNode = function (item) {
        if (flowPageNodeList) {
            for (var i = 0; i < flowPageNodeList.length; i++) {
                if (flowPageNodeList[i].Flowid == item.Flowid) {
                    flowPageNodeList[i] = item;
                }
            }
        }
    }
    var conName = "flow";
    flowNodeData._getNodeForm = function (id) {
        var item = flowNodeData._getflowNode(id);
        item.BtnName = document.getElementById(conName + "BtnName").value;
        item.isEndDel = document.getElementById(conName + "isEndDel").checked;
        item.isEndUpdate = document.getElementById(conName + "isEndUpdate").checked;
        item.isUpdate = document.getElementById(conName + "isUpdate").checked;
        item.isDelete = document.getElementById(conName + "isDelete").checked;
        item.isRevoke = document.getElementById(conName + "isRevoke").checked;
        item.isReturn = document.getElementById(conName + "isReturn").checked;
        item.isDescription = document.getElementById(conName + "isDescription").checked;
        item.execOperator = document.getElementById(conName + "execOperator").value;
        item.execInfluence = document.getElementById(conName + "execInfluence").value;
        item.nextCondition = document.getElementById(conName + "nextCondition").value;
        item.sonFlow = document.getElementById(conName + "sonFlow").value;
        return item;
    }
    flowNodeData._setNodeForm = function (id) {
        var item = flowNodeData._getflowNode(id);
        document.getElementById(conName + "BtnName").value = item.BtnName;
        document.getElementById(conName + "isEndDel").checked = item.isEndDel;
        document.getElementById(conName + "isEndUpdate").checked = item.isEndUpdate;
        document.getElementById(conName + "isUpdate").checked = item.isUpdate;
        document.getElementById(conName + "isDelete").checked = item.isDelete;
        document.getElementById(conName + "isRevoke").checked = item.isRevoke;
        document.getElementById(conName + "isReturn").checked = item.isReturn;
        document.getElementById(conName + "isDescription").checked = item.isDescription;
        document.getElementById(conName + "execOperator").value = item.execOperator;
        document.getElementById(conName + "execInfluence").value = item.execInfluence;
        document.getElementById(conName + "nextCondition").value = item.nextCondition;
        document.getElementById(conName + "sonFlow").value = item.sonFlow;
    }
    flowNodeData._GetFlowDemoData = function (id) {
        var demoData = demo.exportData();
        if (demoData.nodes) {
            for (var item in demoData.nodes) {
                if ((item + "") == id)
                    return demoData.nodes[item];
            }
        }
        return null;
    }
    return flowNodeData;
}
var pageSelectedItemid = "";
function setRightForm(nodeid) {
    var flowNodeData = new flowNodeList();
    if (pageSelectedItemid != "") {
        flowNodeData._updateflowNode(flowNodeData._getNodeForm(pageSelectedItemid));
    }
    pageSelectedItemid = nodeid;
    document.getElementById("flowid").value = pageSelectedItemid;
    document.getElementById("flowname").innerHTML = flowNodeData._GetFlowDemoData(nodeid).name;
    flowNodeData._setNodeForm(nodeid);
}
function ItemNode(id) {
    var item = {
        "Flowid": id,
        "BtnName": "",
        "isEndDel": false,
        "isEndUpdate": false,
        "isUpdate": false,
        "isDelete": false,
        "isRevoke": false,
        "isReturn": false,
        "isDescription": false,
        "execOperator": "",
        "execInfluence": "",
        "nextCondition": "",
        "sonFlow": ""
    };
    return item;
}