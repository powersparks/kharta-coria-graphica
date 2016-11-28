/// <reference path="https://d3js.org/d3.v4.js" />
(function ($) {

 var api = {
     tfc_layout: function () {
         //var margin = { top: 20, right: 10, bottom: 20, left: 10 };
         //var width = 600 - margin.left - margin.right,
         //     height = 50 - margin.top - margin.bottom;

         var _svgSelector = "", _svg,
             _cntrl_width = 600,
             _cntrl_height = 80,
             _margin = { top: 35, right: 10, bottom: 35, left: 10 },
             _width = (_cntrl_width - _margin.left - _margin.right),
             _height = (_cntrl_height - _margin.top - _margin.bottom);
             //x = d3.scaleTime().range([0, _width]),
             //y = d3.scaleLinear().range([_height, 0]),
             //_xAxis = d3.axisTop(x).tickSize([_height]),
         //    _yAxis = d3.axisRight(y).ticks(0).tickSize(_width);
         //x.domain([new Date().setFullYear(2016, 01, 01),  Date.now()]);
         //y.domain([0, 100]);

         function tfc_layout(opts) {
             if (!arguments.length) {
                 return {
                     svgSelector: _svgSelector,
                     svg: _svg,
                     cntrl_width: _cntrl_width,
                     cntrl_height: _cntrl_height,
                     width: _width,
                     height: _height,
                     margin: _margin   
                     
                   

                 };
             }

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
         tfc_layout.svgSelector = function (value) { if (!arguments.length) { return _svgSelector; } _svgSelector = value; return tfc_layout; };// gets or sets selection string
         tfc_layout.svg = function (value) { if (!arguments.length) { return _svg; }  _svg = value; return tfc_layout; };//gets svg if selector is defined or sets (using selector string)
         tfc_layout.cntrl_width  = function (value) { if (!arguments.length) { return _cntrl_width;  } _cntrl_width = value;  return tfc_layout; };//get or set control width
         tfc_layout.cntrl_height = function (value) { if (!arguments.length) { return _cntrl_height; } _cntrl_height = value; return tfc_layout; };//get or set control height
         tfc_layout.width = function (value) { if (!arguments.length) { return _width; }  _cntrl_width = (value + _margin.left + _margin.right); return tfc_layout; };//gets width based on margins or sets width,  as a number calculated base on margins                      /*if (checkNum(value)) tfc_layout({ "width" : value, "height": (value * 0.02)  }); return tfc_layout; };
         tfc_layout.height = function (value) { if (!arguments.length) { return _height; } _cntrl_height = (value + _margin.top + _margin.bottom); return tfc_layout; };//if (checkNum(value)) tfc_layout({ "height": value, "width" : (value / 0.02)  }); return tfc_layout; };
         tfc_layout.margin = function (value) { if (!arguments.length) { return _margin; } _margin = value; return tfc_layout; };//margin
         tfc_layout.margin.top = function (value) { if (!arguments.length) { return _margin.top; } _margin.top = value; return tfc_layout; };//top margin
         tfc_layout.margin.bottom = function (value) { if (!arguments.length) { return _margin.bottom; } _margin.bottom = value; return tfc_layout; };//bottom margin
         tfc_layout.margin.right  = function (value) { if (!arguments.length) { return _margin.right;  } _margin.right  = value; return tfc_layout; };//right margin
         tfc_layout.margin.left   = function (value) { if (!arguments.length) { return _margin.left;   } _margin.left   = value; return tfc_layout; };//left margin
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
            * Class Name: Time Filter Control
            * 1) "Variable" - define layout dimensions (required as input) 
            *   a) Height, width, margins required for svg control
            *   b) Use the default settings, or set your own
            *   c) Reusing and naming "tfc_layout" class 
            *   d) Based on "Margin Convention" example: 
            *          https://bl.ocks.org/mbostock/3019563
            *   
            */
        // var tfc_layout = new $.coria.timeFilterControl.tfc_layout();

         var _timeFilterChart, _x, _y, _xAxis, _yAxis, X_MinFromDateExample, X_MaxToDateExample, Y_MinFromValueExample, Y_MinFromValueExample;

         function TimeFilterChart(tfc_layout) {

             if (!arguments.length) { 
                 return {
                     chart: _timeFilterChart,
                     x: _x,
                     y: _y,
                     xAxis: _xAxis,
                     yAxis: _yAxis
                 };
             }
             /*****
             * 2) "Algebra" - group and assemble what is broken
             *    a) Select the html  
             *    b) Append svg control (or chart)
             *    c) set svg control's class name, width, & height
             *    d) Append svg group ("g") orgin within the control
             */
             _timeFilterChart = d3.select("svg.timeline-svg-filter2")
                 .append("svg")
                   .attr("class", "time-filter-control")
                   .attr("width", tfc_layout.cntrl_width())
                   .attr("height", tfc_layout.cntrl_height())
                          .append("g")
                          .attr("class", "timeline-axis-container layout")
                          .attr("transform", "translate(" + tfc_layout.margin.left() + "," + tfc_layout.margin.top() + ")");
             /***
             * 5) "Scaling" - arrange layout to domains
             *   a)  Scale "time" range along layout's x-axis
             *   b)  Scale "linear" range along the layout's y-axis 
             */
                 _x = d3.scaleTime().range([0, tfc_layout.width()]),
                 _y = d3.scaleLinear().range([tfc_layout.height(), 0]);
             /******
             *   c) Scaling axis' ticks and tick length to layout, use axis orienation methods, e.g., "axisTop" or "axisRight"
             *   d) Methods instantiate 
             */
                 _xAxis = d3.axisTop(_x).tickSize(tfc_layout.height()),
                 _yAxis = d3.axisRight(_y).ticks(0).tickSize(tfc_layout.width());
             /****
             *    f) Domain Data Charateistics,  EXAMPLES provided. 
             *    e) Scaling Domain - overide d3 default domains on x and y axis 
             **/
             var X_MinFromDateExample = new Date().setFullYear(2016, 01, 01), X_MaxToDateExample = Date.now();
             var Y_MinFromValueExample = 0, Y_MinFromValueExample = 100;

             _x.domain([X_MinFromDateExample, X_MaxToDateExample]);
             _y.domain([Y_MinFromValueExample, Y_MinFromValueExample]);

             /****
             *   h) Append "g" x-axis, class, add xAxis' translate ticks to layout
             *   i) Append "g" y-axis, class, add yAxis' 
             */

             _timeFilterChart
                          .append("g")
                          .attr("class", "axis axis--x")
                          .attr("transform", "translate(0," + (tfc_layout.height()) + ")")
                          .call(_xAxis);

             _timeFilterChart
                            .append("g")
                            .attr("class", "axis axis--y")
                            .call(_yAxis);

             TimeFilterChart.chart = function (value) { if (!arguments.length) { return _timeFilterChart; } _timeFilterChart = value; return TimeFilterChart; };
             TimeFilterChart.x = function (value) { if (!arguments.length) { return _x; } _x = value; return TimeFilterChart; };
             return TimeFilterChart;//_timeFilterChart;
         }
         
         return TimeFilterChart;
     },
     tfc_data: function(){},
     register: function (opts) {
         //new layout and main time chart
         var tfc_layout = new $.coria.timeFilterControl.tfc_layout();
         
         tfc_layout
              .margin.top(10)
              .margin.bottom(60)
             .margin.left(10)
             .margin.right(10);
         
         var _timeFilterChart = new $.coria.timeFilterControl.tfc_chart();
         _timeFilterChart(tfc_layout);
      
        //new layout and bass time chart
         var tfc_layout_bass = new $.coria.timeFilterControl.tfc_layout();
         tfc_layout_bass
             .margin.top(35)
             .margin.bottom(35)
             .margin.left(10)
             .margin.right(10);
         var _timeFilterChartBass = new $.coria.timeFilterControl.tfc_chart();
         _timeFilterChartBass(tfc_layout_bass);

        // context.select(".brush").call(brush.move, x.range().map(t.invertX, t));
        //_timeFilterChartBass().chart.select(".brush").call(brush.move, _timeFilterChartBass().x.range().map(t.invertX, t));

         /**
         * 6) Brushes (bass & focus) to interact between time filter charts
         *    a) Setup brush on X axis, 
         *    b) extend of rendered brush
         *    c) event method on "brush" and "end"    
         */

         //Setup Brush for focus time filter
         var brushFocus = d3.brushX()
         .extent([[0, 0], [tfc_layout.width(), tfc_layout.height()]])
         .on("brush end", brushedFocus);

         //Append brush to focus chart
         _timeFilterChart().chart
            .append("g")
         .attr("class", "brush")
          .call(brushFocus)
         .call(brushFocus.move, _timeFilterChart().x.range());

         //method for brushing
         function brushedFocus() {
             console.info("brushed focus returned domain");
             console.info(_timeFilterChart().x.domain());
             console.info(_timeFilterChart().y.domain());
         }
        
       //Setup Brush for bass time filter
       var brushBass = d3.brushX()
       .extent([[0, 0], [tfc_layout_bass.width(), tfc_layout_bass.height()]])
       .on("brush end", brushedBass);

        // Append brush to bass chart
         _timeFilterChartBass().chart
              .append("g")
               .attr("class", "brush")
               .call(brushBass)
               .call(brushBass.move, _timeFilterChartBass().x.range());

         //method for brushing 
         function brushedBass() {
             console.info("brushed Bass returned domain");
             console.info(_timeFilterChartBass().x.domain());
             console.info(_timeFilterChartBass().y.domain());
             
             if (d3.event.sourceEvent && d3.event.sourceEvent.type === "zoom") return; // ignore brush-by-zoom
             /****
             var s = d3.event.selection || x2.range();
             x.domain(s.map(x2.invert, x2));
             //focus.select(".line").attr("d", line); 

             focus.select(".axis--x").call(xAxis);
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
})(jQuery);