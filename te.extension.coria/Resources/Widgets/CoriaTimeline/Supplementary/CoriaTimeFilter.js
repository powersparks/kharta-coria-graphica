/// <reference path="d3.js" />
/// <reference path="C:\Program Files\Telligent\9.2\Utility\JQuery\jquery.min.js" />
/// <reference path="C:\Program Files\Telligent\9.2\Utility\JQuery\evolution\telligent.evolution.js" />
(function ($) {
    var date_dynField = "Date";
    var api = {
        tfc_layout_array: [],
        tfc_charts_array: [],
        tfc_clipPath: function () {
            var _id = "clip", _svg, _hgt, _wit, _shp = "rect";
            function clipPath(svg, width, height) {
                if (!arguments.length) {
                    return {
                        id: _id,
                        svg: _svg,
                        height: _hgt,
                        width: _wit,
                        shape: _shp
                    };
                }
                svg.append("defs").append("clipPath")
                  .attr("id", "clip")
                  .append("rect")
                  .attr("width", width)
                  .attr("height", height);
                _id = "clip";
                _svg = svg;
                _hgt = height;
                _wit = width;
                _shp = "rect";
                return clipPath();
            }

            clipPath.id = function (value) { if (!arguments.length) { return _id; } _id = value; return clipPath; };
            clipPath.svg = function (value) { if (!arguments.length) { return _svg; } _svg = value; return clipPath; };
            clipPath.height = function (value) { if (!arguments.length) { return _hgt; } _hgt = value; return clipPath; };
            clipPath.width = function (value) { if (!arguments.length) { return _wit; } _wit = value; return clipPath; };
            clipPath.shape = function (value) { if (!arguments.length) { return _shp; } _shp = value; return clipPath; };

            return clipPath;
        },
        tfc_layout: function () {
            //var margin = { top: 20, right: 10, bottom: 20, left: 10 };
            //var width = 600 - margin.left - margin.right,
            //     height = 50 - margin.top - margin.bottom;

            var _svgSelector = "", _svg,
                _cntrl_width = 600,
                _cntrl_height = 80,
                _margin = { top: 35, right: 10, bottom: 35, left: 10 },
                _width = (_cntrl_width - _margin.left - _margin.right),
                _height = (_cntrl_height - _margin.top - _margin.bottom),
                _name = 'ChartName';
            //x = d3.scaleTime().range([0, _width]),
            //y = d3.scaleLinear().range([_height, 0]),
            //_xAxis = d3.axisTop(x).tickSize([_height]),
            //    _yAxis = d3.axisRight(y).ticks(0).tickSize(_width);
            //x.domain([new Date().setFullYear(2016, 01, 01),  Date.now()]);
            //y.domain([0, 100]);

            function tfc_layout(opts) {
                if (!arguments.length) {
                    return {
                        chartName: _name,
                        svgSelector: _svgSelector,
                        svg: _svg,
                        cntrl_width: _cntrl_width,
                        cntrl_height: _cntrl_height,
                        width: _width,
                        height: _height,
                        margin: _margin  
                     };
                }
                _name = opts.name ? opts.name : _name;
                _svgSelector = opts.svgSelector ? opts.svgSelector : _svgSelector;
                _svg = opts.svg ? opts.svg : _svgSelector;
                _cntrl_width  = opts.width   ?    opts.width  : _cntrl_width;
                _cntrl_height = opts.height  ?    opts.height : _cntrl_height;
                _margin       = opts.margin  ?  opts.margin :  _margin;
                _margin.top = opts.margin ? opts.margin.top ? opts.margin.top : _margin.top : _margin.top;
                _margin.bottom = opts.margin ? opts.margin.bottom ? opts.margin.bottom : _margin.bottom : _margin.bottom;
                _margin.right = opts.margin ? opts.margin.right ?opts.margin.right : _margin.right : _margin.right ;
                _margin.left = opts.margin ? opts.margin.left ? pts.margin.left : _margin.left : _margin.left;
            }
            tfc_layout.chartName = function (value) { if (!arguments.length) { return _name; } _name = value; return tfc_layout;};
            tfc_layout.svgSelector = function (value) { if (!arguments.length) { return _svgSelector; } _svgSelector = value; return tfc_layout; };// gets or sets selection string
            tfc_layout.svg = function (value) { if (!arguments.length) { return _svg; }  _svg = value; return tfc_layout; };//gets svg if selector is defined or sets (using selector string)
            tfc_layout.cntrl_width = function (value) { if (!arguments.length) { return _cntrl_width; } _cntrl_width = value; _width = (_cntrl_width - _margin.right - _margin.left);  return tfc_layout; };//get or set control width
            tfc_layout.cntrl_height = function (value) { if (!arguments.length) { return _cntrl_height; } _cntrl_height = value; _height = (_cntrl_height - _margin.top - _margin.bottom); return tfc_layout; };//get or set control height
            tfc_layout.width = function (value) { if (!arguments.length) { return _width; } _width = value; _cntrl_width = (_width + _margin.left + _margin.right); return tfc_layout; };//gets width based on margins or sets width,  as a number calculated base on margins                      /*if (checkNum(value)) tfc_layout({ "width" : value, "height": (value * 0.02)  }); return tfc_layout; };
            tfc_layout.height = function (value) { if (!arguments.length) { return _height; } _height = value; _cntrl_height = (_height + _margin.top + _margin.bottom); return tfc_layout; };//if (checkNum(value)) tfc_layout({ "height": value, "width" : (value / 0.02)  }); return tfc_layout; };
            tfc_layout.margin = function (value) { if (!arguments.length) { return _margin; } _margin = value; return tfc_layout; };//margin
            tfc_layout.margin.top = function (value) { if (!arguments.length) { return _margin.top; } _margin.top = value; _height = (_cntrl_height - _margin.top - _margin.bottom); return tfc_layout; };//top margin
            tfc_layout.margin.bottom = function (value) { if (!arguments.length) { return _margin.bottom; } _margin.bottom = value; _height = (_cntrl_height - _margin.top - _margin.bottom); return tfc_layout; };//bottom margin
            tfc_layout.margin.right = function (value) { if (!arguments.length) { return _margin.right; } _margin.right = value; _width = (_cntrl_width - _margin.left - _margin.right); return tfc_layout; };//right margin
            tfc_layout.margin.left = function (value) { if (!arguments.length) { return _margin.left; } _margin.left = value; _width = (_cntrl_width - _margin.left - _margin.right); return tfc_layout; };//left margin
            // tfc_layout.x = function (value) { if (!arguments.length) { return x; } x = value; return tfc_layout; };// x scales or sets range
            //tfc_layout.y = function (value) { if (!arguments.length) { return y; } y = value; return tfc_layout; };// y scales or sets range
            // tfc_layout.xAxis = _xAxis ;//d3.axisTop(x).tickSize([_height]);//function (value) { if (!arguments.length) { return _xAxis; } _xAxis = value; return tfc_layout; };// xAxis setup
            //tfc_layout.yAxis = _yAxis ;//d3.axisRight(y).ticks(0).tickSize(_width);//function (value) { if (!arguments.length) { return _yAxis; } _yAxis = value; return tfc_layout; };// yAxis setup
            //   var _xAxis = d3.axisTop(x).tickSize([height]),
            //   _yAxis = d3.axisRight(y).ticks(0).tickSize(width);
            return tfc_layout;
        },
        tfc_chart: function () {
            /**
               * Class Name: Time Filter Control (servos)
               *  Description: TFC uses a paradigm of gears or virtual servo-mechanisms. 
               *               using d3js, the objects consist of master and slave gear ratios, 
               *               feedback, and adjustment surfaces( or pentiometers) 
               *               to sense, move (rotate) and zoom on axis. 
               * 1) "Variable" - define layout dimensions (required as input) 
               *   a) Height, width, margins required for svg control
               *   b) Use the default settings, or set your own
               *   c) Reusing and naming "tfc_layout" class 
               *   d) Based on "Margin Convention" example: 
               *          https://bl.ocks.org/mbostock/3019563
               *   e) other examples that helped form this:
             
               brush and zoom
               http://bl.ocks.org/tnightingale/4718717
               */
            // var tfc_layout = new $.coria.timeFilterControl.tfc_layout();

            var _timeFilterChart, _x, _y, _xAxis, _yAxis, X_MinFromDateExample, X_MaxToDateExample, Y_MinFromValueExample, Y_MinFromValueExample;
            var _tfc_layout , _name;
            function TimeFilterChart(tfc_layout,data) {
                
                if (!arguments.length) { 
                    return {
                        chartName: _name,
                        chart: _timeFilterChart,
                        x: _x,
                        y: _y,
                        xAxis: _xAxis,
                        yAxis: _yAxis,
                        settings: _tfc_layout
                    };
                }
                /*****
                * 2) "Algebra" - group and assemble what is broken
                *    a) Select the html  
                *    b) Append svg control (or chart)
                *    c) set svg control's class name, width, & height
                *    d) Append svg group ("g") orgin within the control
                */
                _name = tfc_layout.chartName();
                _tfc_layout = tfc_layout;
                //_timeFilterChart = d3.select("svg.timeline-svg-filter2")
                _timeFilterChart = api.svgMain  
                      .append("svg")
                      .attr("class", "time-filter-control")
                      .attr("width", _tfc_layout.cntrl_width())
                      .attr("height", _tfc_layout.cntrl_height())
                             .append("g")
                             .attr("class", "timeline-axis-container layout")
                             .attr("transform", "translate(" + _tfc_layout.margin.left() + "," + _tfc_layout.margin.top() + ")");
                /***
                * 5) "Scaling" - arrange layout to domains
                *   a)  Scale "time" range along layout's x-axis
                *   b)  Scale "linear" range along the layout's y-axis 
                */
                _x = d3.scaleTime().range([0, _tfc_layout.width()]),
                _y = d3.scaleLinear().range([_tfc_layout.height(), 0]);
                /******
                *   c) Scaling axis' ticks and tick length to layout, use axis orienation methods, e.g., "axisTop" or "axisRight"
                *   d) Methods instantiate 
                */
                _xAxis = d3.axisTop(_x).tickSize(_tfc_layout.height()),
                _yAxis = d3.axisRight(_y).ticks(0).tickSize(_tfc_layout.width());
                /****
                *    f) Domain has Data Charateistics,  EXAMPLES provided. 
                *    e) Scaling Domain - overide d3 default domains on x and y axis 
                **/
                var X_MinFromDateExample = new Date().setFullYear(2016, 01, 01), X_MaxToDateExample = Date.now();
                var Y_MinFromValueExample = 0, Y_MaxToValueExample =  1000;
                if (data == null) {
                    _x.domain([X_MinFromDateExample, X_MaxToDateExample]);
                    _y.domain([Y_MinFromValueExample, Y_MaxToValueExample]);
                } else {
                    _x.domain(d3.extent(data, function (d) { return d[date_dynField]; }));
                    _y.domain([0, 100]);
                    //                y.domain([0, d3.max(data, function (d) { return d.price; })]);
                }

                /****
                *   h) Append "g" x-axis, class, add xAxis' translate ticks to layout
                *   i) Append "g" y-axis, class, add yAxis' 
                */

                _timeFilterChart
                             .append("g")
                             .attr("class", "axis axis--x")
                             .attr("transform", "translate(0," + (_tfc_layout.height()) + ")")
                             .call(_xAxis);

                _timeFilterChart
                               .append("g")
                               .attr("class", "axis axis--y")
                               .call(_yAxis);
                /***
                        chart: _timeFilterChart,
                        x: _x,
                        y: _y,
                        xAxis: _xAxis,
                        yAxis: _yAxis,
                        settings: _tfc_layout
          
                */
                //return TimeFilterChart;//_timeFilterChart;
            }

            TimeFilterChart.chart = function (value) { if (!arguments.length) { return TimeFilterChart; } _timeFilterChart = value; return TimeFilterChart; };
            TimeFilterChart.settings = function (value) { if (!arguments.length) { return _tfc_layout; } _tfc_layout = value; return TimeFilterChart; };
            TimeFilterChart.chartName = function (value) { if (!arguments.length) { return _name; } _name = value; return TimeFilterChart;};
            TimeFilterChart.x = function (value) { if (!arguments.length) { return _x; } _x = value; return TimeFilterChart; };
            TimeFilterChart.y = function (value) { if (!arguments.length) { return _y; } _y = value; return TimeFilterChart; };
            TimeFilterChart.xAxis = function (value) { if (!arguments.length) { return _xAxis; } _xAxis = value; return TimeFilterChart; };
            TimeFilterChart.yAxis = function (value) { if (!arguments.length) { return _yAxis; } _yAxis = value; return TimeFilterChart; };
            TimeFilterChart.settings = function (value) { if (!arguments.length) { return _tfc_layout; } _tfc_layout = value; return TimeFilterChart; };

            return TimeFilterChart;
        },
        tfc_data: function (d) { 
            console.info("data results");
            console.info(d);
            opts.data = d;
            api.tfc_setup(opts);
        },
        tfc_addEditIconBtn: function (opts) {
            var  iconId = opts.timelineEditImgId, btnId = opts.timelineEditBtnId, url = opts.timelineEditSvgUrl;
            $('#' + iconId).attr("src", url);
            $("span.ui-loading").hide();
            $('#' + btnId).on('click', function () { 
                $("span.ui-loading").show();
                var DataChoice = "restApi";
                switch (DataChoice) {
                    case "restApi": 
                        tfc_dataRestApi(opts);
                        break;
                    case "csvFile": 
                        tfc_dataCsvFie(opts);
                        break;
                    case "jsonUrl": 
                        tfc_dataJsonUrl(opts);
                        break;
                    default:
                        tfc_dataRestApi(opts);
                        break;
                }
                     function tfc_dataRestApi(opts) {
                        var search = api.tfc_data_SearchTelligentRestCall();
                        search(function (results) {
                            opts.data = results.SearchResults.map(function (d) {
                                return type(d, "Date", "Rating");
                            }); 
                            api.tfc_setup(opts );
                            $("span.ui-loading").hide();
                        });
                     }
                     function tfc_dataCsvFile(opts) { }
                     function tfc_dataJsonUrl(opts) { }
 
            });
        },
        tfc_setup: function (opts) {
            var data = opts.data ? opts.data : null;
            var svgMainHeight = 80;
           
            api.svgMain = d3.selectAll('#' + opts.tfcSvgId);
                api.svgMain.attr("viewBox", "0 0 600 " + svgMainHeight);
             
            //api.tfc_charts_array.push( svgMain );
           
            //new layout and main time chart setup
            var _tfc_layout_slave = new $.coria.timeFilterControl.tfc_layout()
               .margin.top(10)
               .margin.bottom(60)
               .margin.left(30)
               .margin.right(30);
               _tfc_layout_slave.chartName("SlavedChart");
             
            //api.tfc_layout_array.push(_tfc_layout_slave);

            //console.info("height of slave")
            //console.info(_tfc_layout_slave.height());
            //console.info(_tfc_layout_slave().height);
            //console.info(_tfc_layout_slave.cntrl_height());
            //console.info(_tfc_layout_slave().cntrl_height);
            var _slaveChart = new $.coria.timeFilterControl.tfc_chart();
            _slaveChart(_tfc_layout_slave, data);
            var line = d3.line()
                  .defined(function (d) { return d; })
                     .x(function (d) { return x(d[date_dynField]); })
                     //.y(y(100));
            .y(function (d) { return y(d.price); });
            if (data != null) {
                console.info("slave chart");
                console.info(_slaveChart());
                _slaveChart().chart.selectAll("circle")
              .data(data).enter().append("circle")
              .attr("clip-path", "url('#clip')")
              .attr("d", line)
              .attr("class", "dot")
              .attr("r", 3.5)
              .attr("opacity", 0.7)
              .style("fill", "steelblue");
            }
            api.tfc_charts_array.push( _slaveChart );
            var _dist_between_charts = _tfc_layout_slave.cntrl_height();
            //new layout and master time chart setup
            var _tfc_layout_master = new $.coria.timeFilterControl.tfc_layout()
                .margin.top(35)
                .margin.bottom(35)
                .margin.left(30)
                .margin.right(30);
                _tfc_layout_master.chartName("MasteredChart");


            api.tfc_layout_array.push(_tfc_layout_master);

            var _masterChart = new $.coria.timeFilterControl.tfc_chart();
            _masterChart(_tfc_layout_master,data);
            api.tfc_charts_array.push( _masterChart );
            var masterChart = _masterChart;
            var slaveChart = _slaveChart;
        
            // context.select(".brush").call(brush.move, x.range().map(t.invertX, t));
            //_timeFilterChartBass().chart.select(".brush").call(brush.move, _timeFilterChartBass().x.range().map(t.invertX, t));

            /**
            * 6) Brushes (master & slave) to interact between time filter charts
            *    a) Setup brush on X axis, 
            *    b) extend of rendered brush
            *    c) event method on "brush" and "end"    
            */

            //Setup Brush for slave time filter
            var brush_sensor_slave = d3.brushX()
                .extent([[0, 0], [_tfc_layout_slave().width, _tfc_layout_slave().height]])
                .on("brush end", slave_feedback);
            //Append brush to slave chart
            _slaveChart().chart
               .append("g")
                   .attr("class", "brush brush-sensor brush-sensor-slave")
                   .call(brush_sensor_slave)
                   .call(brush_sensor_slave.move, [_tfc_layout_slave().width - Math.floor(_tfc_layout_slave().width / 2) - (_tfc_layout_slave().width / 5), _tfc_layout_slave().width - Math.floor(_tfc_layout_slave().width / 2) + (_tfc_layout_slave().width / 5)]);
            //                .call(brush_sensor_slave.move, _slaveChart().x.range());
            /*
            *[width - Math.floor(width / 2) - (width / 5), width - Math.floor(width / 2) + (width / 5)]
            * Sets up a zoom servo as an adjustment surface for changing Gear(master/slave) Ratio
            * uses a lazy setting, "Infinity" which assumes a level of precision in our data that is not realistic. 
            * realistic would calculate data resolution, e.g. date/time may only go to seconds, days, weeks, etc 
            * Spatial, temporal, or aspatial data resolutions need to be reported back to user in a meaningful anology
            */
            var zoom_ratio = d3.zoom()
                 .scaleExtent([1, Infinity])
                 .translateExtent([[0, 0], [_tfc_layout_slave().width, _tfc_layout_slave().height]])
                 .extent([[0, 0], [_tfc_layout_slave().width, _tfc_layout_slave().height]])
                 .on("zoom", slave_feedback);
            //append zoom servo
            _slaveChart().chart
                .append("rect")
                   .attr("class", "zoom servo ratio-adjustment-surface")
                   .attr("width", _tfc_layout_slave().width)
                   .attr("height", _tfc_layout_slave().height)
                   .attr("transform", "translate(" + 0 + "," + Math.floor(_tfc_layout_slave().margin.top / 2) + ")")
                   .call(zoom_ratio);

            //method for brushing event
            function slave_feedback() {
                if (d3.event.sourceEvent) {
                    console.info(d3.event.sourceEvent.type);
                }
                if (d3.event.sourceEvent && (d3.event.sourceEvent.type === "wheel" || d3.event.sourceEvent.type === "zoom")) {
                    //change resolution of axis and data on slave chart, slave chart brush, and master chart brush
                    console.info("zoom");

                    var transform = d3.event.transform;
                    _slaveChart().x.domain(transform.rescaleX(_masterChart().x).domain());
                     
                    _slaveChart().chart.select(".axis--x").call(_slaveChart().xAxis);
                    _masterChart().chart.select(".brush").call(brush_sensor_master.move, _slaveChart().x.range().map(transform.invertX, transform));
                    /**/
                }
                if (d3.event.sourceEvent && (d3.event.sourceEvent.type === "mousemove" || d3.event.sourceEvent.type === "touchmove")) {
                    //console.info("slaved chart's brush event");
                    //console.info(_slaveChart().x.domain());
                    //console.info(_slaveChart().y.domain());
                    //_slaveChart().chart.select(".line").attr("d", line);
                    _slaveChart().chart.selectAll(".dot")
                    .attr('cx', function (d) { return _slaveChart().x(d[date_dynField]); })
                    .attr('cy', _tfc_layout_slave.height() * 0.5);
                }
            }

            //Setup Brush sensor for master chart's time filter
            var brush_sensor_master = d3.brushX()
            .extent([[0, 0], [_tfc_layout_master.width(), _tfc_layout_master.height()]])
            .on("brush end", master_feedback);

            // Append brush to master chart
            _masterChart().chart
                 .append("g")
                  .attr("class", "brush brush-sensor brush-sensor-master")
                  .call(brush_sensor_master)
                  .call(brush_sensor_master.move, _masterChart().x.range());
             
            //method for brushing event
            function master_feedback() {
                 
                if (d3.event.sourceEvent && (d3.event.sourceEvent.type === "mousemove" || d3.event.sourceEvent.type === "touchmove")) {
                    //array from brush sensor or use default x-axis range
                    var sensor = d3.event.selection || _masterChart().x.range();

                    //provide feedback from master to slave 
                    //drive domain mapping sensor output over master x-min and max values
                    _slaveChart().x.domain(sensor.map(_masterChart().x.invert, _masterChart().x));
                    //rescale the x axis after setting the domain.
                    _slaveChart().chart.select(".axis--x").call(_slaveChart().xAxis);

                    _slaveChart().chart.select(".zoom").call(zoom_ratio.transform, d3.zoomIdentity
                        .scale(_tfc_layout_slave().width / (sensor[1] - sensor[0]))
                        .translate(-sensor[0], 0));
                    //
                    //_slaveChart().x.domain(s
                    //console.info("brushed Bass brush event2");
                    //console.info(_masterChart().x.domain());
                    //console.info(_masterChart().y.domain());

                }
                _slaveChart().chart.selectAll(".dot")
                 .attr('cx', function (d) { return _slaveChart().x(d[date_dynField]); })
                 .attr('cy', _tfc_layout_slave.height() * 0.5);
                /****
                var s = d3.event.selection || x2.range();
                x.domain(s.map(x2.invert, x2));
                //slave.select(".line").attr("d", line); 
   
                slave.select(".axis--x").call(xAxis);
                svg.select(".zoom").call(zoom.transform, d3.zoomIdentity
                    .scale(width / (s[1] - s[0]))
                    .translate(-s[0], 0));
                */
            }

            /*
                  var tfc_layout3 = new $.coria.timeFilterControl.tfc_layout();
                  tfc_layout3.margin.top(60);
                  tfc_layout3.margin.bottom(10);
                  tfc_layout3.margin.left(10);
                  tfc_layout3.margin.right(10);
                  var _timeFilterChart3 = new $.coria.timeFilterControl.tfc_chart();
                  _timeFilterChart2(tfc_layout3);
                  */
        },/****tfc_setup end**/
        tfc_data_SearchTelligentRestCall: function () {

                   var _baseUrl = $.telligent.evolution.site.getBaseUrl(true),
                    _searchRestApi = "api.ashx/v2/search.json",
                    x3rand = Math.floor(Math.random() * 1000 + 0),
                    y3rand = Math.floor(Math.random() * 1000 + 0),
                    callbackname = "search" + x3rand + y3rand;
                //var loading = $.telligent.evolution.ui.components.loading = {
                //    setup: function () {
                //        // setup is called once per page if at least one element matches this component 
                //    }, add: function (elm, options) {
                //        // add is called for each unique instance of an element matching the component 
                //        // elm is the matching element 
                //        // options is an object containing all data attributes defined on the element
                //        //$(elm).html('loading: ' + options.make + ' ' + options.model); 

                //    }
                //};
                //   Query: "{!geofilt pt=21.5,-158 sfield=GeoTagGeoHash d=10}"
                var searchResult = function (callback) {
                    $.telligent.evolution.get({
                        url: _baseUrl + _searchRestApi,
                        callback: callbackname,
                        data: {
                            Query: "*:*"
                        },

                        cache: false
                    }).done(function (r) {
                        if (typeof callback !== 'undefined' && typeof callback === 'function') {
                            // invoke currently-defined UI components against all matching elements in div.mydiv
                            //$.telligent.evolution.ui.suppress(function () {
                            //    // DOM manipulation without automatic ui component rendering

                            //});


                            callback(r);

                        }
                    });
                };
                return searchResult;
        },
        getLayouts: function () { return api.tfc_layout_array; } ,
        register: function (opts) {
            if (typeof d3 === 'undefined') return;
             
            var tfc_clipPathId = opts.tfc_clipPathId, tfcSvgId = opts.tfcSvgId;
            api.tfc_addEditIconBtn(opts);
            //api.tfc_setup(opts);
          
           
        }
 };
 $.coria = $.coria ? $.coria : {};
 $.coria.timeFilterControl = api;

 function checkMargins(margin) {
     if (checkNum(margin.top) && checkNum(margin.bottom) && checkNum(margin.right) && checkNum(margin.left))
         return true;
     return false;
 }
 function checkNum(value) {
     if (isNaN(value)) {
         throw new Error("value must be a number not: " + value);
     }
     return true;
 }
    /**
    *type: convert date
    */
 function type(d, x_dynField, y_dynField) {
     var parseDate = d3.utcParse("%Y-%m-%dT%H:%M:%S%Z");
     var value_dynField = y_dynField ? y_dynField : "price";
     var date_dynField = x_dynField ? x_dynField : "date";
     d[date_dynField] = parseDate(d[date_dynField]);
     //hard coded y value...
     var num = isNaN(d[y_dynField]) ? Math.floor(Math.random() * 1000) : d[y_dynField] * 100;
     d[y_dynField] = num <1000 ? num : 1000 ;//d.price;
     return d;
 }
})(jQuery);