/*
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
var showControllersOnly = false;
var seriesFilter = "";
var filtersOnlySampleSeries = true;

/*
 * Add header in statistics table to group metrics by category
 * format
 *
 */
function summaryTableHeader(header) {
    var newRow = header.insertRow(-1);
    newRow.className = "tablesorter-no-sort";
    var cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Requests";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 3;
    cell.innerHTML = "Executions";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 7;
    cell.innerHTML = "Response Times (ms)";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Throughput";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 2;
    cell.innerHTML = "Network (KB/sec)";
    newRow.appendChild(cell);
}

/*
 * Populates the table identified by id parameter with the specified data and
 * format
 *
 */
function createTable(table, info, formatter, defaultSorts, seriesIndex, headerCreator) {
    var tableRef = table[0];

    // Create header and populate it with data.titles array
    var header = tableRef.createTHead();

    // Call callback is available
    if(headerCreator) {
        headerCreator(header);
    }

    var newRow = header.insertRow(-1);
    for (var index = 0; index < info.titles.length; index++) {
        var cell = document.createElement('th');
        cell.innerHTML = info.titles[index];
        newRow.appendChild(cell);
    }

    var tBody;

    // Create overall body if defined
    if(info.overall){
        tBody = document.createElement('tbody');
        tBody.className = "tablesorter-no-sort";
        tableRef.appendChild(tBody);
        var newRow = tBody.insertRow(-1);
        var data = info.overall.data;
        for(var index=0;index < data.length; index++){
            var cell = newRow.insertCell(-1);
            cell.innerHTML = formatter ? formatter(index, data[index]): data[index];
        }
    }

    // Create regular body
    tBody = document.createElement('tbody');
    tableRef.appendChild(tBody);

    var regexp;
    if(seriesFilter) {
        regexp = new RegExp(seriesFilter, 'i');
    }
    // Populate body with data.items array
    for(var index=0; index < info.items.length; index++){
        var item = info.items[index];
        if((!regexp || filtersOnlySampleSeries && !info.supportsControllersDiscrimination || regexp.test(item.data[seriesIndex]))
                &&
                (!showControllersOnly || !info.supportsControllersDiscrimination || item.isController)){
            if(item.data.length > 0) {
                var newRow = tBody.insertRow(-1);
                for(var col=0; col < item.data.length; col++){
                    var cell = newRow.insertCell(-1);
                    cell.innerHTML = formatter ? formatter(col, item.data[col]) : item.data[col];
                }
            }
        }
    }

    // Add support of columns sort
    table.tablesorter({sortList : defaultSorts});
}

$(document).ready(function() {

    // Customize table sorter default options
    $.extend( $.tablesorter.defaults, {
        theme: 'blue',
        cssInfoBlock: "tablesorter-no-sort",
        widthFixed: true,
        widgets: ['zebra']
    });

    var data = {"OkPercent": 99.98333333333333, "KoPercent": 0.016666666666666666};
    var dataset = [
        {
            "label" : "FAIL",
            "data" : data.KoPercent,
            "color" : "#FF6347"
        },
        {
            "label" : "PASS",
            "data" : data.OkPercent,
            "color" : "#9ACD32"
        }];
    $.plot($("#flot-requests-summary"), dataset, {
        series : {
            pie : {
                show : true,
                radius : 1,
                label : {
                    show : true,
                    radius : 3 / 4,
                    formatter : function(label, series) {
                        return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'
                            + label
                            + '<br/>'
                            + Math.round10(series.percent, -2)
                            + '%</div>';
                    },
                    background : {
                        opacity : 0.5,
                        color : '#000'
                    }
                }
            }
        },
        legend : {
            show : true
        }
    });

    // Creates APDEX table
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.90025, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [1.0, 500, 1500, "Signout"], "isController": false}, {"data": [0.9033333333333333, 500, 1500, "Share Skill Add"], "isController": false}, {"data": [0.9883333333333333, 500, 1500, "Update Skills"], "isController": false}, {"data": [1.0, 500, 1500, "Add Educatiom"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Education"], "isController": false}, {"data": [0.27166666666666667, 500, 1500, "Categories"], "isController": false}, {"data": [0.9916666666666667, 500, 1500, "Add Description"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Skills"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Language Request"], "isController": false}, {"data": [0.925, 500, 1500, "Delete Manage Listing"], "isController": false}, {"data": [0.9983333333333333, 500, 1500, "Update Certifications"], "isController": false}, {"data": [0.9233333333333333, 500, 1500, "Update Education"], "isController": false}, {"data": [1.0, 500, 1500, "Add Language"], "isController": false}, {"data": [1.0, 500, 1500, "Add Certifications"], "isController": false}, {"data": [1.0, 500, 1500, "View Manage listing"], "isController": false}, {"data": [1.0, 500, 1500, "Add Skills"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Certifications"], "isController": false}, {"data": [0.03833333333333333, 500, 1500, "SignIn"], "isController": false}, {"data": [1.0, 500, 1500, "Update Language"], "isController": false}, {"data": [0.965, 500, 1500, "Toggle Checkbox"], "isController": false}]}, function(index, item){
        switch(index){
            case 0:
                item = item.toFixed(3);
                break;
            case 1:
            case 2:
                item = formatDuration(item);
                break;
        }
        return item;
    }, [[0, 0]], 3);

    // Create statistics table
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 6000, 1, 0.016666666666666666, 647.481333333333, 7, 15990, 86.0, 625.7000000000016, 3978.0999999999676, 11439.939999999999, 116.67250029168125, 56.04015281059192, 77.16699040247151], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["Signout", 300, 0, 0.0, 37.73333333333334, 10, 226, 23.0, 85.50000000000017, 145.5499999999999, 218.91000000000008, 6.209380303845676, 1.2588346222109534, 3.2623501987001697], "isController": false}, {"data": ["Share Skill Add", 300, 0, 0.0, 383.31999999999994, 25, 8857, 87.5, 561.0, 1009.6499999999999, 8781.76, 6.268937415108139, 1.3713300595549056, 10.248243391495143], "isController": false}, {"data": ["Update Skills", 300, 0, 0.0, 187.76999999999998, 42, 596, 125.0, 448.80000000000007, 484.74999999999994, 524.8400000000001, 7.869058860560277, 1.5923061096684503, 5.074364655466373], "isController": false}, {"data": ["Add Educatiom", 300, 0, 0.0, 86.9933333333333, 14, 300, 52.5, 185.90000000000003, 204.95, 294.8800000000001, 7.788161993769471, 1.5747704114745587, 5.272981162383177], "isController": false}, {"data": ["Delete Education", 300, 0, 0.0, 75.88999999999997, 7, 246, 39.0, 184.90000000000003, 198.0, 236.97000000000003, 7.700205338809035, 1.6393516587525667, 4.473839354466119], "isController": false}, {"data": ["Categories", 300, 0, 0.0, 2243.0299999999993, 413, 11531, 1592.5, 4443.8, 10662.45, 11363.590000000002, 6.088403620570686, 32.17816444778179, 4.399822928928035], "isController": false}, {"data": ["Add Description", 300, 0, 0.0, 171.49666666666658, 30, 595, 112.5, 414.90000000000003, 454.0, 549.5600000000004, 7.633199328278459, 1.6399451681848252, 4.7558409877359935], "isController": false}, {"data": ["Delete Skills", 300, 0, 0.0, 75.02999999999996, 11, 405, 38.0, 199.80000000000007, 208.95, 399.7900000000002, 7.812093120149992, 1.4715826975157544, 5.052888277303265], "isController": false}, {"data": ["Delete Language Request", 300, 0, 0.0, 60.55000000000001, 9, 378, 37.0, 139.0, 199.95, 233.93000000000006, 7.988071147087017, 1.539237863124401, 4.6337053333688365], "isController": false}, {"data": ["Delete Manage Listing", 300, 0, 0.0, 285.91333333333324, 45, 8102, 130.0, 556.3000000000002, 742.8, 2196.84, 6.297757998152658, 1.1623791617684103, 3.67164211415736], "isController": false}, {"data": ["Update Certifications", 300, 0, 0.0, 197.18666666666675, 39, 512, 132.0, 432.80000000000007, 450.0, 491.84000000000015, 7.629704984740591, 1.3062889432858595, 5.212216071655646], "isController": false}, {"data": ["Update Education", 300, 1, 0.3333333333333333, 224.8266666666666, 43, 615, 135.5, 529.9000000000001, 551.8, 592.98, 7.700205338809035, 1.6367448184034907, 5.534271929543121], "isController": false}, {"data": ["Add Language", 300, 0, 0.0, 104.39333333333335, 17, 328, 61.0, 264.90000000000003, 291.84999999999997, 319.98, 8.017531669250094, 1.6207314995456732, 4.708707832794377], "isController": false}, {"data": ["Add Certifications", 300, 0, 0.0, 89.83333333333339, 13, 281, 57.5, 222.90000000000003, 238.89999999999998, 276.96000000000004, 7.6850167789533, 1.5535141340266927, 4.950681805082358], "isController": false}, {"data": ["View Manage listing", 300, 0, 0.0, 117.9833333333333, 30, 423, 71.5, 268.2000000000003, 377.95, 414.98, 6.295247088448221, 2.7603183034309096, 3.8238707900535096], "isController": false}, {"data": ["Add Skills", 300, 0, 0.0, 92.04000000000005, 13, 301, 52.0, 235.90000000000003, 248.74999999999994, 280.0, 7.941760423560556, 1.6061641710787558, 4.895867802779616], "isController": false}, {"data": ["Delete Certifications", 300, 0, 0.0, 74.82666666666664, 11, 262, 43.0, 203.0, 228.84999999999997, 253.99, 7.639224873316188, 1.5048427513304983, 4.468648143668355], "isController": false}, {"data": ["SignIn", 300, 0, 0.0, 8041.749999999996, 195, 15990, 8455.5, 12787.300000000001, 13652.099999999999, 15847.200000000004, 7.966646307459437, 3.8277245930371513, 2.7824134623044854], "isController": false}, {"data": ["Update Language", 300, 0, 0.0, 178.74999999999994, 42, 410, 123.0, 353.0, 364.9, 387.99, 8.007046200656578, 1.681870671190648, 5.067454151319828], "isController": false}, {"data": ["Toggle Checkbox", 300, 0, 0.0, 220.31000000000003, 32, 8651, 82.5, 449.90000000000003, 525.55, 2255.2900000000045, 6.296700528922845, 1.1683331059524809, 3.6833238445554533], "isController": false}]}, function(index, item){
        switch(index){
            // Errors pct
            case 3:
                item = item.toFixed(2) + '%';
                break;
            // Mean
            case 4:
            // Mean
            case 7:
            // Median
            case 8:
            // Percentile 1
            case 9:
            // Percentile 2
            case 10:
            // Percentile 3
            case 11:
            // Throughput
            case 12:
            // Kbytes/s
            case 13:
            // Sent Kbytes/s
                item = item.toFixed(2);
                break;
        }
        return item;
    }, [[0, 0]], 0, summaryTableHeader);

    // Create error table
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": [{"data": ["500/Internal Server Error", 1, 100.0, 0.016666666666666666], "isController": false}]}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 6000, 1, "500/Internal Server Error", 1, "", "", "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Update Education", 300, 1, "500/Internal Server Error", 1, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
