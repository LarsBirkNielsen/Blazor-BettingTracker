﻿//<script type="text/javascript">

//    var _oStudents = [],

//    _oStudent = null,

//    _headers = [];


//    $(document).ready(function () {

//        Init();
//        concole.log("Document ready");

//    });

//    function Init() {

//        _oStudent = NewStudentObj();


//    $("#btnDownloadExcel").click(function () {

//        GenerateAndDownloadExcel();

//        });

//    $("#btnSave").click(function () {

//        Save();

//        });

//    $("#input").on("change", function (e) {

//            var file = e.target.files[0];

//    if (!file) return;


//    var FR = new FileReader();

//    FR.onload = function (e) {

//                var data = new Uint8Array(e.target.result);

//    var workbook = XLSX.read(data, {type: 'array' });

//    var firstSheet = workbook.Sheets[workbook.SheetNames[0]];

//    var result = XLSX.utils.sheet_to_json(firstSheet, {header: 1 });

//    GenerateTable(result);

//            };

//    FR.readAsArrayBuffer(file);

//    $("#input").val("");

//        });

//    }

//    function GenerateAndDownloadExcel() {

//        var StudentId = 0,

//    name = "";


//    $.ajax({

//        url: "../YouTube/GenerateAndDownloadExcel?StudentId=" + StudentId + "&name=" + name,

//            //data: {'Id': groupId },

//    type: 'GET',

//    success: function (result) {

//        ExcelFromBase64("Student List Excel.xlsx", result);

//            },

//    error: function (result) {

//    }

//        });

//    }

//    function Reset() {

//        _oStudents = [];

//    _oStudent = null;

//    _headers = [];

//    $("#tblMain thead tr,#tblMain tbody tr").remove();

//    }

//    function GenerateTable(exportStatus) {

//        Reset();


//        if (exportStatus.length > 0) {

//            var sTemp = "";

//    var headers = exportStatus[2];

//    sTemp = "<tr>";

//        sTemp += "<th style='text-align:center;vertical-align:middle;'>Serial</th>";


//        $.map(headers, function (header) {

//            _headers.push(header);

//        sTemp += "<th style='text-align:center;vertical-align:middle;min-width:100px;'>" + header + "</th>";

//            });

//        sTemp += "</tr>";

//    $("#tblMain thead").append(sTemp);


//    exportStatus = exportStatus.slice(3);

//            exportStatus = exportStatus != null ? exportStatus.filter(x => x.length > 0) : exportStatus;

//    var nSL = 0;

//    for (var i = 0; i < exportStatus.length; i++) {

//        nSL++;


//    _oStudent = NewStudentObj();


//    sTemp = "<tr>";

//        sTemp += "<td style='text-align:center;vertical-align:middle;'>" + nSL + "</td>";


//        var valueIndex = 0;

//        var es = exportStatus[i];

//        for (var j = 0; j < _headers.length; j++) {

//            propValue = es[j];

//        propValue = typeof (propValue) == "undefined" ? "" : propValue;


//        var propName = _headers[valueIndex];

//        _oStudent[propName] = propValue;


//        sTemp += "<td style='text-align:center;vertical-align:middle;' title='" + _headers[valueIndex] + "'>" + propValue + "</td>";

//        valueIndex++;

//                }

//        sTemp += "</tr>";

//    $("#tblMain tbody").append(sTemp);


//    _oStudents.push(_oStudent);

//            }

//        }

//    }

//    function ExcelFromBase64(fileName, bytesBase64) {

//        var link = document.createElement('a');

//    link.download = fileName;

//    link.href = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + bytesBase64;

//    document.body.appendChild(link);

//    link.click();

//    document.body.removeChild(link);

//    }

//    function Save() {

//        if (_oStudents.length > 0) {

//            var ajaxRequest = $.ajax({

//        url: "../YouTube/SaveStudents/",

//    type: "POST",

//    data: {students: _oStudents },

//    dataType: "json",

//    beforeSend: function () {


//    },

//            });

//    ajaxRequest.done(function (data) {

//        alert("Successfully saved.");

//            });

//    ajaxRequest.fail(function (jqXHR, textStatus) {alert("Error Found"); alerts('error title', 'error info', 'error'); });

//        }

//    else {

//        alert("No Data Found.");

//        }

//    }

//    function NewStudentObj() {

//        var oStudent = {

//        StudentId: 0,

//    Name: "",

//    Country: ""

//        };

//    return oStudent;

//    }

//</script>