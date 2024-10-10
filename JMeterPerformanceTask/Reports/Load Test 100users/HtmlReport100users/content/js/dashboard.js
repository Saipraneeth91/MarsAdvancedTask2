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

    var data = {"OkPercent": 100.0, "KoPercent": 0.0};
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
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.89375, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [1.0, 500, 1500, "Signout"], "isController": false}, {"data": [0.765, 500, 1500, "Share Skill Add"], "isController": false}, {"data": [0.99, 500, 1500, "Update Skills"], "isController": false}, {"data": [1.0, 500, 1500, "Add Educatiom"], "isController": false}, {"data": [0.985, 500, 1500, "Delete Education"], "isController": false}, {"data": [0.205, 500, 1500, "Categories"], "isController": false}, {"data": [0.995, 500, 1500, "Add Description"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Skills"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Language Request"], "isController": false}, {"data": [0.965, 500, 1500, "Delete Manage Listing"], "isController": false}, {"data": [1.0, 500, 1500, "Update Certifications"], "isController": false}, {"data": [0.995, 500, 1500, "Update Education"], "isController": false}, {"data": [0.93, 500, 1500, "Add Language"], "isController": false}, {"data": [0.995, 500, 1500, "Add Certifications"], "isController": false}, {"data": [0.985, 500, 1500, "View Manage listing"], "isController": false}, {"data": [1.0, 500, 1500, "Add Skills"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Certifications"], "isController": false}, {"data": [0.075, 500, 1500, "SignIn"], "isController": false}, {"data": [1.0, 500, 1500, "Update Language"], "isController": false}, {"data": [0.99, 500, 1500, "Toggle Checkbox"], "isController": false}]}, function(index, item){
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
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 2000, 0, 0.0, 619.8649999999996, 11, 14664, 86.5, 1072.2000000000007, 3545.1499999999714, 10746.85, 87.24480893386843, 41.90392029096144, 57.6928877077517], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["Signout", 100, 0, 0.0, 41.620000000000005, 11, 403, 28.0, 68.80000000000001, 90.69999999999993, 402.95, 8.721437292865863, 1.7612363454997384, 4.582161390197105], "isController": false}, {"data": ["Share Skill Add", 100, 0, 0.0, 700.6999999999998, 32, 3075, 142.0, 2807.9, 2994.0499999999997, 3074.83, 6.164088023176971, 1.3483942550699626, 10.076839209763916], "isController": false}, {"data": ["Update Skills", 100, 0, 0.0, 173.23999999999998, 44, 771, 125.5, 325.30000000000007, 372.6499999999997, 770.8799999999999, 5.929088106249259, 1.2021457740424524, 3.822582692250682], "isController": false}, {"data": ["Add Educatiom", 100, 0, 0.0, 78.26999999999998, 24, 391, 58.5, 153.9, 194.79999999999995, 390.0599999999995, 5.929088106249259, 1.1985558964781218, 4.012440894402941], "isController": false}, {"data": ["Delete Education", 100, 0, 0.0, 68.49, 14, 715, 42.5, 84.60000000000002, 141.24999999999983, 714.9, 5.992329817833173, 1.2757108401246406, 3.4818713296979866], "isController": false}, {"data": ["Categories", 100, 0, 0.0, 2070.4299999999985, 597, 4613, 1612.0, 4423.900000000001, 4444.7, 4612.089999999999, 6.980802792321117, 36.894633507853406, 5.0447207678883075], "isController": false}, {"data": ["Add Description", 100, 0, 0.0, 150.72, 37, 803, 112.5, 269.40000000000003, 372.7999999999997, 799.7999999999984, 6.0540016951204745, 1.3006644266860394, 3.7719268373895143], "isController": false}, {"data": ["Delete Skills", 100, 0, 0.0, 47.559999999999974, 14, 160, 38.0, 82.80000000000001, 120.69999999999993, 159.93999999999997, 5.97442944198829, 1.1273234929501732, 3.8634838204982676], "isController": false}, {"data": ["Delete Language Request", 100, 0, 0.0, 49.010000000000005, 17, 214, 35.5, 99.00000000000006, 133.89999999999998, 213.88999999999993, 5.9662311317940455, 1.1469263655211503, 3.460880168247718], "isController": false}, {"data": ["Delete Manage Listing", 100, 0, 0.0, 226.60000000000002, 53, 988, 157.0, 442.9, 634.5999999999999, 986.6799999999994, 7.6359193646915084, 1.4093640233659133, 4.451800645235187], "isController": false}, {"data": ["Update Certifications", 100, 0, 0.0, 151.17, 58, 391, 118.5, 266.30000000000007, 329.34999999999985, 390.91999999999996, 6.024096385542169, 1.029508659638554, 4.114681381777108], "isController": false}, {"data": ["Update Education", 100, 0, 0.0, 180.74000000000004, 56, 760, 135.5, 377.00000000000017, 425.49999999999966, 757.3499999999987, 5.938242280285036, 1.2644860636876485, 4.267415751187649], "isController": false}, {"data": ["Add Language", 100, 0, 0.0, 229.47, 27, 1466, 67.0, 1150.000000000001, 1261.8, 1465.1199999999994, 5.525777753218766, 1.117027338785434, 3.244343848427916], "isController": false}, {"data": ["Add Certifications", 100, 0, 0.0, 84.30999999999999, 14, 683, 61.0, 164.00000000000006, 201.34999999999985, 678.959999999998, 6.003121623244087, 1.2135216562612559, 3.867909750570296], "isController": false}, {"data": ["View Manage listing", 100, 0, 0.0, 179.22, 37, 700, 89.0, 413.0, 465.44999999999965, 699.81, 7.4487895716946, 3.2661196461824953, 4.524557728119181], "isController": false}, {"data": ["Add Skills", 100, 0, 0.0, 81.22999999999998, 16, 401, 53.5, 179.80000000000007, 213.84999999999997, 399.9999999999995, 5.948839976204639, 1.2042334361986913, 3.6653916939321833], "isController": false}, {"data": ["Delete Certifications", 100, 0, 0.0, 42.34, 17, 152, 35.5, 66.9, 86.64999999999992, 151.5899999999998, 6.080875646093037, 1.1966379408634844, 3.5570747187595013], "isController": false}, {"data": ["SignIn", 100, 0, 0.0, 7560.229999999999, 684, 14664, 8359.5, 12006.0, 12747.25, 14656.179999999997, 5.137426149499101, 2.4683727202671464, 1.7906739660929873], "isController": false}, {"data": ["Update Language", 100, 0, 0.0, 146.60999999999993, 39, 429, 104.5, 276.70000000000005, 338.74999999999994, 428.6399999999998, 5.889975262103899, 1.2382177487042054, 3.723798813169984], "isController": false}, {"data": ["Toggle Checkbox", 100, 0, 0.0, 135.34000000000006, 39, 615, 112.0, 269.9, 315.9, 614.9499999999999, 7.592437931819907, 1.408753131880647, 4.441279610507935], "isController": false}]}, function(index, item){
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
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": []}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 2000, 0, "", "", "", "", "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
