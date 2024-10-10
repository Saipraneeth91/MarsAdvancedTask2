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

    var data = {"OkPercent": 99.81098479660321, "KoPercent": 0.18901520339679495};
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
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.9551705245856732, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [1.0, 500, 1500, "Signout"], "isController": false}, {"data": [0.9883203559510567, 500, 1500, "Share Skill Add"], "isController": false}, {"data": [0.9902173913043478, 500, 1500, "Update Skills"], "isController": false}, {"data": [0.9956474428726877, 500, 1500, "Add Educatiom"], "isController": false}, {"data": [0.9989082969432315, 500, 1500, "Delete Education"], "isController": false}, {"data": [0.41727221911682505, 500, 1500, "Categories"], "isController": false}, {"data": [0.9848484848484849, 500, 1500, "Add Description"], "isController": false}, {"data": [0.999727965179543, 500, 1500, "Delete Skills"], "isController": false}, {"data": [0.9929691725256896, 500, 1500, "Delete Language Request"], "isController": false}, {"data": [0.9810161920714685, 500, 1500, "Delete Manage Listing"], "isController": false}, {"data": [0.9871654833424358, 500, 1500, "Update Certifications"], "isController": false}, {"data": [0.9885558583106268, 500, 1500, "Update Education"], "isController": false}, {"data": [0.9924771628156905, 500, 1500, "Add Language"], "isController": false}, {"data": [0.9972692517749864, 500, 1500, "Add Certifications"], "isController": false}, {"data": [0.9919220055710306, 500, 1500, "View Manage listing"], "isController": false}, {"data": [0.9937669376693767, 500, 1500, "Add Skills"], "isController": false}, {"data": [0.9945145364783324, 500, 1500, "Delete Certifications"], "isController": false}, {"data": [0.8329775880469584, 500, 1500, "SignIn"], "isController": false}, {"data": [0.9840884573894283, 500, 1500, "Update Language"], "isController": false}, {"data": [0.984375, 500, 1500, "Toggle Checkbox"], "isController": false}]}, function(index, item){
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
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 36505, 69, 0.18901520339679495, 59453.00213669361, 7, 21652805, 37.0, 226.0, 733.9500000000007, 3621.9900000000016, 1.6667434481184837, 0.7893132741476562, 1.1007943920343817], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["Signout", 1774, 0, 0.0, 32.136978579481394, 8, 450, 15.0, 79.0, 119.0, 221.5, 7.474131248104082, 1.516191537252688, 3.9268384877734337], "isController": false}, {"data": ["Share Skill Add", 1798, 0, 0.0, 36202.70244716351, 19, 21647687, 46.0, 165.10000000000014, 278.0999999999999, 751.0899999999999, 0.08213730964404395, 0.017971417720914415, 0.13427525033606402], "isController": false}, {"data": ["Update Skills", 1840, 2, 0.10869565217391304, 23621.479347826087, 19, 21645995, 51.0, 185.9000000000001, 284.0, 741.9499999999996, 0.08404198463502416, 0.01700024081282941, 0.05420310136961487], "isController": false}, {"data": ["Add Educatiom", 1838, 2, 0.1088139281828074, 35381.59357997824, 11, 21650397, 20.0, 114.0, 192.0999999999999, 403.6099999999999, 0.08393874955650153, 0.016969603406305697, 0.05685958854245658], "isController": false}, {"data": ["Delete Education", 1832, 1, 0.05458515283842795, 11852.744541484715, 8, 21646154, 15.0, 101.0, 148.3499999999999, 264.6700000000001, 0.08368430188472396, 0.01781372976982249, 0.048625155880283946], "isController": false}, {"data": ["Categories", 1789, 13, 0.7266629401900503, 182952.87311347123, 370, 21650626, 988.0, 3375.0, 3795.5, 5217.899999999987, 0.08172905607280236, 0.4288763821615713, 0.059062013177611075], "isController": false}, {"data": ["Add Description", 1815, 3, 0.1652892561983471, 202822.49752066119, 25, 21650688, 45.0, 168.80000000000018, 281.59999999999945, 1220.1999999999996, 0.08289939420868028, 0.01781991739760486, 0.05165020850111135], "isController": false}, {"data": ["Delete Skills", 1838, 0, 0.0, 36.50489662676819, 7, 571, 15.0, 96.20000000000027, 139.0, 256.0, 7.448714103925367, 1.4030452647170868, 4.818603814426514], "isController": false}, {"data": ["Delete Language Request", 1849, 11, 0.5949161709031909, 46864.538128718224, 8, 21646236, 15.0, 99.0, 127.0, 277.5, 0.08444825352196945, 0.016244876057116067, 0.04898658456254869], "isController": false}, {"data": ["Delete Manage Listing", 1791, 0, 0.0, 24287.592964824118, 33, 21650126, 54.0, 226.79999999999995, 418.39999999999986, 897.2399999999998, 0.08181575612490977, 0.015100759675398385, 0.04769922500641712], "isController": false}, {"data": ["Update Certifications", 1831, 8, 0.4369197160021846, 94674.35117422175, 18, 21650428, 51.0, 204.0, 299.0, 801.7600000000004, 0.0836265553180529, 0.014333306056316435, 0.05713880340276043], "isController": false}, {"data": ["Update Education", 1835, 2, 0.10899182561307902, 35492.25613079019, 20, 21650902, 52.0, 212.60000000000036, 378.0, 737.4399999999973, 0.08380214200101704, 0.017839084845216076, 0.06022828512674104], "isController": false}, {"data": ["Add Language", 1861, 2, 0.10746910263299302, 81468.38151531435, 10, 21650310, 18.0, 100.79999999999995, 179.89999999999986, 528.0, 0.08497545392006244, 0.017188401665591956, 0.04992969648169658], "isController": false}, {"data": ["Add Certifications", 1831, 0, 0.0, 46.535226652102644, 10, 927, 19.0, 110.0, 180.59999999999945, 361.76000000000045, 7.486150009199256, 1.5134412946930516, 4.821647829486682], "isController": false}, {"data": ["View Manage listing", 1795, 0, 0.0, 36260.620055710315, 24, 21650713, 39.0, 154.80000000000018, 225.39999999999964, 656.8399999999992, 0.081990635801524, 0.03595097214344168, 0.04980290573100385], "isController": false}, {"data": ["Add Skills", 1845, 3, 0.16260162601626016, 58713.22926829268, 10, 21650308, 20.0, 115.0, 175.39999999999964, 506.31999999999607, 0.08425200748103029, 0.01703328464887782, 0.05195478028651893], "isController": false}, {"data": ["Delete Certifications", 1823, 9, 0.4936917169500823, 95029.74876577072, 9, 21649921, 15.0, 93.0, 144.0, 342.52, 0.08326625900456307, 0.016478204388702792, 0.04868079060308066], "isController": false}, {"data": ["SignIn", 1874, 11, 0.5869797225186766, 150636.75880469583, 78, 21652805, 238.0, 1222.5, 1849.5, 2760.25, 0.08556299744621391, 0.04092347878612953, 0.02989436411283339], "isController": false}, {"data": ["Update Language", 1854, 2, 0.10787486515641856, 58468.4687162891, 19, 21650461, 48.0, 180.0, 309.0, 812.1500000000003, 0.08465787055866386, 0.017779394260607335, 0.053565557757581046], "isController": false}, {"data": ["Toggle Checkbox", 1792, 0, 0.0, 12169.28236607143, 24, 21649892, 39.0, 166.70000000000005, 300.39999999999964, 840.2199999999971, 0.08185862567681255, 0.015188612186127328, 0.04788409841836984], "isController": false}]}, function(index, item){
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
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": [{"data": ["Non HTTP response code: org.apache.http.NoHttpResponseException/Non HTTP response message: localhost:60190 failed to respond", 1, 1.4492753623188406, 0.0027393507738665934], "isController": false}, {"data": ["500/Internal Server Error", 35, 50.72463768115942, 0.09587727708533078], "isController": false}, {"data": ["401/Unauthorized", 33, 47.82608695652174, 0.09039857553759759], "isController": false}]}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 36505, 69, "500/Internal Server Error", 35, "401/Unauthorized", 33, "Non HTTP response code: org.apache.http.NoHttpResponseException/Non HTTP response message: localhost:60190 failed to respond", 1, "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Update Skills", 1840, 2, "401/Unauthorized", 2, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Add Educatiom", 1838, 2, "401/Unauthorized", 2, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Delete Education", 1832, 1, "401/Unauthorized", 1, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Categories", 1789, 13, "500/Internal Server Error", 13, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Add Description", 1815, 3, "401/Unauthorized", 3, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": ["Delete Language Request", 1849, 11, "500/Internal Server Error", 7, "401/Unauthorized", 4, "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": ["Update Certifications", 1831, 8, "401/Unauthorized", 6, "500/Internal Server Error", 2, "", "", "", "", "", ""], "isController": false}, {"data": ["Update Education", 1835, 2, "401/Unauthorized", 2, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Add Language", 1861, 2, "401/Unauthorized", 2, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Add Skills", 1845, 3, "401/Unauthorized", 3, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Delete Certifications", 1823, 9, "401/Unauthorized", 6, "500/Internal Server Error", 2, "Non HTTP response code: org.apache.http.NoHttpResponseException/Non HTTP response message: localhost:60190 failed to respond", 1, "", "", "", ""], "isController": false}, {"data": ["SignIn", 1874, 11, "500/Internal Server Error", 11, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Update Language", 1854, 2, "401/Unauthorized", 2, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
