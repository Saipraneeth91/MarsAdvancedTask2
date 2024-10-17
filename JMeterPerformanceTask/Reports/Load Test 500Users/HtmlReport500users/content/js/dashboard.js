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

    var data = {"OkPercent": 99.95, "KoPercent": 0.05};
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
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.99415, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [0.998, 500, 1500, "Signout"], "isController": false}, {"data": [0.995, 500, 1500, "Share Skill Add"], "isController": false}, {"data": [0.998, 500, 1500, "Update Skills"], "isController": false}, {"data": [1.0, 500, 1500, "Add Educatiom"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Education"], "isController": false}, {"data": [0.928, 500, 1500, "Categories"], "isController": false}, {"data": [1.0, 500, 1500, "Add Description"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Skills"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Language Request"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Manage Listing"], "isController": false}, {"data": [0.994, 500, 1500, "Update Certifications"], "isController": false}, {"data": [0.996, 500, 1500, "Update Education"], "isController": false}, {"data": [0.98, 500, 1500, "Add Language"], "isController": false}, {"data": [1.0, 500, 1500, "Add Certifications"], "isController": false}, {"data": [0.998, 500, 1500, "View Manage listing"], "isController": false}, {"data": [1.0, 500, 1500, "Add Skills"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Certifications"], "isController": false}, {"data": [0.998, 500, 1500, "SignIn"], "isController": false}, {"data": [1.0, 500, 1500, "Update Language"], "isController": false}, {"data": [0.998, 500, 1500, "Toggle Checkbox"], "isController": false}]}, function(index, item){
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
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 10000, 5, 0.05, 65.94380000000032, 5, 4211, 35.0, 104.0, 396.0, 462.9899999999998, 66.27432267642224, 31.83166720721661, 43.83681418502465], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["Signout", 500, 0, 0.0, 23.959999999999997, 8, 3876, 14.0, 21.0, 25.0, 51.930000000000064, 3.3645564168819986, 0.6826303828696975, 1.7677063987133936], "isController": false}, {"data": ["Share Skill Add", 500, 0, 0.0, 45.469999999999985, 23, 1754, 31.0, 45.0, 65.94999999999999, 342.98000000000184, 3.35974089678204, 0.7349433211710713, 5.492388926965952], "isController": false}, {"data": ["Update Skills", 500, 0, 0.0, 59.316000000000024, 36, 4211, 47.0, 64.0, 80.0, 113.99000000000001, 3.355479498020267, 0.6791175927790082, 2.1638713949567143], "isController": false}, {"data": ["Add Educatiom", 500, 0, 0.0, 22.584000000000017, 13, 470, 19.0, 31.0, 38.0, 64.96000000000004, 3.35635794886253, 0.6786922874250693, 2.2719068958891326], "isController": false}, {"data": ["Delete Education", 500, 0, 0.0, 17.556000000000008, 5, 89, 16.0, 24.0, 27.0, 47.0, 3.357597571785436, 0.7148272767869135, 1.950737957976309], "isController": false}, {"data": ["Categories", 500, 0, 0.0, 493.77600000000024, 382, 2416, 418.0, 515.2000000000003, 790.6999999999999, 2097.8500000000004, 3.353656491672871, 17.724598567317944, 2.423540824060473], "isController": false}, {"data": ["Add Description", 500, 0, 0.0, 45.762000000000036, 30, 187, 41.0, 58.900000000000034, 74.0, 139.98000000000002, 3.3591088955921773, 0.7216835517873819, 2.0928823001834074], "isController": false}, {"data": ["Delete Skills", 500, 0, 0.0, 17.718000000000004, 10, 92, 16.0, 25.0, 31.94999999999999, 43.0, 3.3564030100222197, 0.6328327433056542, 2.1710224211916573], "isController": false}, {"data": ["Delete Language Request", 500, 0, 0.0, 17.661999999999995, 9, 63, 16.0, 23.0, 30.94999999999999, 47.99000000000001, 3.355209297956007, 0.6466445076062595, 1.9462835185408867], "isController": false}, {"data": ["Delete Manage Listing", 500, 0, 0.0, 55.70399999999996, 41, 169, 51.0, 69.80000000000007, 90.0, 133.99, 3.361095986179173, 0.6203585365115857, 1.95954521850485], "isController": false}, {"data": ["Update Certifications", 500, 3, 0.6, 52.064000000000036, 35, 179, 47.0, 66.0, 83.94999999999999, 148.8800000000001, 3.3576877618996455, 0.572564459210809, 2.2940352355753735], "isController": false}, {"data": ["Update Education", 500, 2, 0.4, 55.69599999999995, 17, 285, 51.0, 67.0, 85.84999999999997, 150.94000000000005, 3.3567184720217513, 0.7132043339431371, 2.4140771856433147], "isController": false}, {"data": ["Add Language", 500, 0, 0.0, 73.97399999999998, 13, 3903, 20.5, 31.0, 43.849999999999966, 2362.9100000000026, 3.3522174918708725, 0.6776455281418658, 1.9696634792665348], "isController": false}, {"data": ["Add Certifications", 500, 0, 0.0, 20.23799999999999, 11, 65, 18.0, 28.0, 32.94999999999999, 50.99000000000001, 3.357755407665084, 0.6787650091666723, 2.162820760078303], "isController": false}, {"data": ["View Manage listing", 500, 0, 0.0, 50.57800000000003, 28, 4141, 38.0, 55.900000000000034, 73.94999999999999, 104.98000000000002, 3.3604408898447473, 1.4734745698635663, 2.041205306136165], "isController": false}, {"data": ["Add Skills", 500, 0, 0.0, 20.309999999999995, 11, 69, 19.0, 27.900000000000034, 34.94999999999999, 49.98000000000002, 3.3555920942250257, 0.6787078244018657, 2.0694631157511494], "isController": false}, {"data": ["Delete Certifications", 500, 0, 0.0, 18.014, 9, 300, 16.0, 24.0, 30.0, 48.99000000000001, 3.3593571534151225, 0.6617671142450181, 1.9650927098590414], "isController": false}, {"data": ["SignIn", 500, 0, 0.0, 132.5099999999999, 86, 841, 113.0, 182.90000000000003, 245.89999999999998, 459.2500000000007, 3.3334444481482715, 1.6016158871962398, 1.1647002816760559], "isController": false}, {"data": ["Update Language", 500, 0, 0.0, 51.72999999999998, 25, 365, 47.0, 61.0, 77.0, 113.98000000000002, 3.3537689655635003, 0.704337335078411, 2.1222676215405873], "isController": false}, {"data": ["Toggle Checkbox", 500, 0, 0.0, 44.25400000000005, 29, 2037, 36.0, 51.0, 61.0, 113.98000000000002, 3.361095986179173, 0.6236408568105888, 1.9661098591028563], "isController": false}]}, function(index, item){
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
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": [{"data": ["500/Internal Server Error", 5, 100.0, 0.05], "isController": false}]}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 10000, 5, "500/Internal Server Error", 5, "", "", "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Update Certifications", 500, 3, "500/Internal Server Error", 3, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Update Education", 500, 2, "500/Internal Server Error", 2, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
