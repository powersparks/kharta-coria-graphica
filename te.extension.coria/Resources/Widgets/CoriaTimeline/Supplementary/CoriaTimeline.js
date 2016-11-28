///<reference path="_agsReferences.js" />
(function ($) {
    // community.apan.org/api.ashx/v2/search.json?formatresponse=true&r=15524474&filters=GeoTagGeoHash:*&_=1479254506086
    // cdnjs.cloudflare.com/ajax/libs/d3/4.3.0/d3.js"

    var svg = d3.select("svg.timeline-svg-filter"), h = 80, w = 600,
       rwidth = svg.attr("width") ? svg.attr("width") : w,
       rheight = svg.attr("height") ? svg.attr("height") : h,
       margin = { top: 20, right: 20, bottom: 50, left: 20 },
       margin2 = { top: 50, right: 20, bottom: 20, left: 20 },
       width = w - margin.left - margin.right,
       height = h - margin.top - margin.bottom,
       height2 = (svg.attr("height") ? +svg.attr("height") : h) - margin2.top - margin2.bottom;

    var date_dynField = "Date";
    var parseDate_type = "utc_parse_with_zone_offset";//"abbreviated_month_name_s_year_century_as_decimal".
    //other types see https: // github.com/d3/d3-time-format/blob/master/README.md#locale_parse
    //var parseDate = d3.timeParse("%b %Y");
    //"2016-08-12T22:06:00-10:00"
    // var timeFormat = d3.timeFormat("%Y-%m-%dT%H:%M:%S%z");
    var parseDate = d3.utcParse("%Y-%m-%dT%H:%M:%S%Z");
    /***uncomment to use amzn.csv file, see line 80 .%L*/
    //  parseDate = d3.timeParse("%Y-%m-%d");//year_century_as_decimal_-_month_as_decimal_-_day_as_decimal

    var x = d3.scaleTime().range([0, width]),
        x2 = d3.scaleTime().range([0, width]),
        y = d3.scaleLinear().range([height, 0]),
        y2 = d3.scaleLinear().range([height2, 0]);

    var xAxis = d3.axisTop(x).tickSize([height]),
        xAxis2 = d3.axisTop(x2).tickSize([height2]).tickPadding(1),
        yAxis = d3.axisRight(y).ticks(0).tickSize(width);
    yAxis2 = d3.axisRight(y2).ticks(0).tickSize(width);


    var focusBrush = d3.brushX()
        .extent([[0, 0], [width, height]])
        .on("brush end", focusBrushed);


    var brush = d3.brushX()
        .extent([[0, 0], [width, height2]])
        .on("brush end", brushed);

    var zoom = d3.zoom()
        .scaleExtent([1, Infinity])
        .translateExtent([[0, 0], [width, height]])
        .extent([[0, 0], [width, height]])
        .on("zoom", zoomed);

    var area = d3.area()
        .curve(d3.curveMonotoneX)
        .x(function (d) { return x(d[date_dynField]); })
        .y0(height)
        .y1(y(100));
    //.y1(function (d) { return y(d.price); });
    var line = d3.line()
    .defined(function (d) { return d; })
       .x(function (d) { return x(d[date_dynField]); })
       .y(y(100));
    // .y(function (d) { return y(d.price); });

    var area2 = d3.area()
        .curve(d3.curveMonotoneX)
        .x(function (d) { return x2(d[date_dynField]); })
        .y0(height2)
        .y1(y2(100));
    // .y1(function (d) { return y2(d.price); });


    // Compute the horizontal scale.
    var focus = svg.append("g")
        .attr("class", "focus")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    var context = svg.append("g")
        .attr("class", "context")
        .attr("transform", "translate(" + margin2.left + "," + margin2.top + ")");
    var dot = function () {
        var _r = 1;
        function dot(opts) { if (!arguments.length) { return { r: _r }; } _r = opts.r ? opts.r : 1; };
        dot.r = function (value) { if (!arguments.length) { return _r; } _r = value; return dot; };
        return dot;
    };

    var api = {
        clipPath: function () {
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
            clipPath.hgt = function (value) { if (!arguments.length) { return _hgt; } _hgt = value; return clipPath; };
            clipPath.wit = function (value) { if (!arguments.length) { return _wit; } _wit = value; return clipPath; };
            clipPath.shp = function (value) { if (!arguments.length) { return _shp; } _shp = value; return clipPath; };

            return clipPath;
        },
        search: function () {
            var _baseUrl = $.telligent.evolution.site.getBaseUrl(),
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
            return searchResult
        },
        register: function (opts) {
            var timelineContainerDivId = '#' + opts.timelineContainerDivId;
            var timelineDiv = '#' + opts.timelineId;
            var timelineSvgFilterId = '#' + opts.timelineSvgFilterId;
            var editSvgUrl = opts.editSvgUrl;
            var timelineEditBtn = '#' + opts.timelineEditBtn;
            var timelineEditSvgUrl = opts.timelineEditSvgUrl, timelineEditImgId = '#' + opts.timelineEditImgId;

            $("span.ui-loading").hide();
            $("input.timeline-search").show();
            $("a.timeline-edit-btn").show();
            $(timelineEditImgId).attr("src", timelineEditSvgUrl);
            $("img.timeline-edit").show();
            /****
            rules for loading timeline data
            1) pick a data source
            2) identify which fields to use
                a) x-axis is Date
                b) y-axis is assumed to be 100 
                    TODO: uncomment and make y field dynamic 
            3) identify date format used
                a) except utc format, assumed to be local time
            4) Optional, identify y-axis data format


                //            d3.csv(opts.tempSourceCsv, type, function (error, data) {
            //                //  d3.csv("amzn.csv", type, function(error, data) {
            */

            $(timelineEditBtn).on('click', function () {
                $("span.ui-loading").show();
                var search = api.search();
                search(function (results) {
                    api.initializeTimeline(results.SearchResults.map(function (d) {
                        return type(d);
                    })
                        );
                    $("span.ui-loading").hide();
                });

            });


        },
        initializeTimeline: function (data) {
            // d3.json(data, type, function (error, data) {
            //  d3.csv("amzn.csv", type, function(error, data) {
            //   if (error) throw error;

            // data = data.filter(function (i) { return i[date_dynField].getMonth() >= Math.floor(Math.random() * 12); });
            var clipPath = api.clipPath();
            var clipRect = clipPath(svg, width, height);
            api.clipRect = clipRect;
            // svg = clipPath.svg();
            /*              svg.append("defs").append("clipPath")
                              .attr("id", "clip")
                            .append("rect")
                              .attr("width", width)
                              .attr("height", height);
          */

            x.domain(d3.extent(data, function (d) { return d[date_dynField]; }));
            y.domain([0, 100]);
            //                y.domain([0, d3.max(data, function (d) { return d.price; })]);
            x2.domain(x.domain());
            y2.domain(y.domain());

            /**** this provides the line graph in the focus view
            *
              focus.append("path")
              .datum(data)
                .attr("class","line")
                .attr("clip-path", "url('#clip')")
                .attr("d",line)
                .attr("stroke-linecap","round")
                .attr("stroke-width", "1")
               .attr('stroke', '#723')
               .attr('stroke-opacity', 0.5);
               ***/


            /***
            *main or focus timline view   .attr("clip-path", "url('/~powersparks/bz.html#clip')")
              focus.append("path")
                  .datum(data)
                  .attr("class", "area")
                  .attr("clip-path", "url('#clip')")
                  .attr("d", area);
            **/

            focus.append("g")
                .attr("class", "axis axis--x")
                .attr("transform", "translate(0," + height + ")")
                .call(xAxis);

            focus.append("g")
                .attr("class", "axis axis--y")
                .call(yAxis);

            /****for the lower or context timeline and brush *
              context.append("path")
                  .datum(data)
                  .attr("class", "area")
                  .attr("d", area2);
            **/

            /**
             * this focus select doesn't use the datum approach
             * which may be a problem...
             * TODO: figure out how to bind the datum
             */
            focus.selectAll("circle")
              .data(data).enter().append("circle")
              .attr("clip-path", "url('#clip')")
              .attr("d", line)
              .attr("class", "dot")
              .attr("r", 3.5)
              .attr("opacity", 0.7)
              .style("fill", "steelblue");

            focus.selectAll("line")
              .data(data).enter().append("line")
              .attr("clip-path", "url('#clip')")
              .attr("d", line)
              .attr("class", "line")
              .attr("stroke-linecap", "round")
              .attr("stroke-width", "1")
             .attr('stroke', "steelblue")
             .attr('stroke-opacity', 0.5);
            //.attr("r", 3.5)
            //    .attr("opacity", 0.7)
            //    .style("fill",  "steelblue");

            /**
             * brush get's drawn by user
             */
            var focusBrushSlider = focus.append("g")
                 .attr("class", "brush focus-brush")

                 .style("cursor", "pointer")
                 .call(focusBrush)
                 .call(focusBrush.move, [width - Math.floor(width / 2) - (width / 5), width - Math.floor(width / 2) + (width / 5)]);

            //console.info(x.range());
            /**
             * context time line setup:
             */
            context.append("g")
                    .attr("class", "axis axis--y")
                    .call(yAxis2);

            context.append("g")
                 .attr("class", "axis axis--x")
                 .attr("transform", "translate(0," + height2 + ")")
                 .call(xAxis2)


            context.append("g")
                 .attr("class", "brush")
                // .attr("stroke-width", "1")
                //.attr('stroke', '#000')
                //.attr('stroke-opacity', 1)
            //.attr("border", "solid 1px #000")
                 .call(brush)
                 .call(brush.move, x.range());

            focus.append("rect")
                .attr("class", "zoom")
                .attr("width", width)
                .attr("height", height)
                .attr("transform", "translate(" + 0 + "," + Math.floor(margin.top / 2) + ")")
              //.attr("transform", "translate(" + margin.left  + "," + margin.top + ")")

                .call(zoom);
            //[width - Math.floor(width / 2) - (width / 5), width - Math.floor(width / 2) + (width / 5)]
            //  }); //end of d3.json
        },
        widgetId: ''
    };


    $.coria = $.coria ? $.coria : {};
    $.coria.timeline = api;

    /**
     * two files for demo
     * TODO: need to add a live data source
     * amzn.csv sp500.csv, change on line 21
     **/

    function focusBrushed() {

        if (!d3.event.sourceEvent) return; // Only transition after input.
        //if (!d3.event.selection) return; // Ignore empty selections.

        var s = d3.event.selection || x.range();
        x.domain(s.map(x.invert, x));

        /*
        var d0 = d3.event.selection.map(x.invert),
            d1 = d0.map(d3.timeDay.round);
          // If empty when rounded, use floor & ceil instead.
          if (d1[0] >= d1[1]) {
            d1[0] = d3.timeDay.floor(d0[0]);
            d1[1] = d3.timeDay.offset(d1[0]);
          }
        */
        //focus.select(".brush").call(brush.move, x.range().map(t.invertX, t));
        focus.select(".focus").transition().call(d3.event.target.move, s.map(x));
    }

    function brushed() {

        if (d3.event.sourceEvent && d3.event.sourceEvent.type === "zoom") return; // ignore brush-by-zoom
        var s = d3.event.selection || x2.range();
        x.domain(s.map(x2.invert, x2));
        //focus.select(".line").attr("d", line);

        focus.select(".line").attr("d", line);
        focus.selectAll(".dot")
        .attr('cx', function (d) { return x(d[date_dynField]); })
          .attr('cy', height * .5);
        focus.selectAll(".line")
        .attr('x1', function (d) { return x(d[date_dynField]); })
        .attr('x2', function (d) { return x(d[date_dynField]); })
        .attr('y1', 0)
        .attr('y2', height);// - margin.top - margin.bottom);
        /**
         * use if change along the yAxis is needed*
        *  .attr('cy', function(d) { return y(d.price); });
        *
        * use if chart of data is needed
        *  focus.select(".area").attr("d", area);
        */
        focus.select(".axis--x").call(xAxis);
        svg.select(".zoom").call(zoom.transform, d3.zoomIdentity
            .scale(width / (s[1] - s[0]))
            .translate(-s[0], 0));

    }

    function zoomed() {

        if (d3.event.sourceEvent == null || d3.event.sourceEvent.type == null || d3.event.sourceEvent.type === 'mousemove') return;

        if (d3.event.sourceEvent && d3.event.sourceEvent.type === "brush") return; // ignore zoom-by-brush

        var t = d3.event.transform;
        x.domain(t.rescaleX(x2).domain());
        /***
        focus.selectAll(".line").enter().attr("d", line)
        .attr('x1', function(d) { return x(d.date); })
         .attr('x2', function(d) { return x(d.date); })
         .attr('y1', function(d) { return y(d.price); })
         .attr('y2', height2 - margin.top - margin.bottom);
         **/

        focus.select(".line").attr("d", line);
        focus.selectAll(".dot")
        .attr('cx', function (d) { return x(d[date_dynField]); })
        .attr('cy', height * 0.5);
        //  .attr('cy', function(d) { return y(d.price); })
        focus.selectAll(".line")
        .attr('x1', function (d) { return x(d[date_dynField]); })
        .attr('x2', function (d) { return x(d[date_dynField]); })
        .attr('y1', 0)
        .attr('y2', height);//- margin.top - margin.bottom);

        focus.select(".area").attr("d", area);
        focus.select(".axis--x").call(xAxis);
        context.select(".brush").call(brush.move, x.range().map(t.invertX, t));
    }

    function type(d) {
        d[date_dynField] = parseDate(d[date_dynField]);
        //d.price = +d.close;
        d.price = +100;//d.price;
        return d;
    }
})(jQuery);

/*
  var searchApiOptions = function () {
        function searchApiOptions(searchOpts) {
            var _query, _filters, _sort, _startDate, _endDate, _collapse, _fieldFacets, _dateRangeFacets, _fieldFilters, _dateRangeFilters, _tags, _logicallyOrTags, _mlt, _id, _guidId, _applicationId, _containerId, _category, _isContent, _isApplication, _isContainer, _pageSize, _pageIndex;
            if (!arguments.length) {
                searchOpts = {
                    query: _query,
                    filters: _filters,
                    sort: _sort,
                    startDate: _startDate,
                    endDate: _endDate,
                    collapse: _collapse,
                    fieldFacets: _fieldFacets,
                    dateRangeFacets: _dateRangeFacets,
                    fieldFilters: _fieldFilters,
                    dateRangeFilters: _dateRangeFilters,
                    tags: _tags,
                    logicallyOrTags: _logicallyOrTags,
                    mlt: _mlt,
                    id: _id,
                    guidId: _guidId,
                    applicationId: _applicationId,
                    containerId: _containerId,
                    category: _category,
                    isContent: _isContent,
                    isApplication: _isApplication,
                    isContainer: _isContainer,
                    pageSize: _pageSize,
                    pageIndex: _pageIndex
                };
                return searchOpts;
            }

            //_query, _filters, _sort, _startDate, _endDate, _collapse, _fieldFacets, _dateRangeFacets, _fieldFilters, _dateRangeFilters, _tags, _logicallyOrTags, _mlt, _id, _guidId, _applicationId, _containerId, _category, _isContent, _isApplication, _isContainer, _pageSize,


        }

        //add accessors as you need them
        searchApiOptions.query = function (value) { if (!arguments.length) { return _query; } _query = value; return searchApiOptions; };

        return searchApiOptions;

    };
                        Query: query, //Query is not required but you should use either Query or Filters otherwise you'll get all documents in the search index.
                        Filters: _string, //Filters is not required but you should use either Query or Filters otherwise you'll get all documents in the search index.
                        Sort: _string, //Two options for sort are titlesort and date and it also supports asc and desc. To sort by title ascending use sort=titlesort+asc, to sort by date descending use sort=date+desc.
                        StartDate: _DateTime, //? Start Date
                        EndDate: _DateTime, //? End Date
                        Collapse: _bool, //? Result collapsing will default to false if not specified.
                        FieldFacets: _string, //Field Facets
                        DateRangeFacets: _string, //Date Range Facets
                        FieldFilters: _string, //Field Filters
                        DateRangeFilters: _string, //Date Range Filters
                        Tags: _string, //Comma delimited list of tags.
                        LogicallyOrTags: _bool, //? Whether to AND or OR tags specified in the Tags parameter. Default is False, logically AND tags.
                        Mlt: _bool, //? More Like This. Can be used in conjunction with Filters. If using MLT, Id should be set.
                        Id: _string, //Used when using MLT.
                        GuidId: _Guid, //? The GUID representing the content.
                        ApplicationId: _Guid, //? The GUID representing the application.
                        ContainerId: _Guid, //? The GUID representing the container.
                        Category: _string, //Category
                        IsContent: _bool, //? Is Content
                        IsApplication: _bool, //? Is Application
                        IsContainer: _bool, //? Is Container
                        PageSize: _int, //? Specify the number of results to return per page. If not set the default is 20. The max is 100.
                        PageIndex: _int */ //? Specify the page number of paged results to return. Zero-based index. If not specified the default is 0.
/***
    *following is just a template for functional programming style, and not part of the timeline
    *
         Options = function () {
                 var _cx = 0,
                     _cy = 0
                     _r =  1;
                 function Options(opts) {
                     if (!arguments.length) {
                         return {
                             cx: _cx,
                             cy: _cy,
                             r: _r
                         };
                     }
                     _cx = opts.x ?;
                     _cy = opts.y;
                     _r  = opts.r;
                 };
                 Options.cx  = function (value) { if (!arguments.length) { return _cx;   } _cx = value; return Options; };
                 Options.cy  = function (value) { if (!arguments.length) { return _cy;   } _cy = value; return Options; };
                 Options.r   = function (value) { if (!arguments.length) { return _r;    } _r  = value; return Options; };
                 return Options;
             };
    ***/
//            d3.csv(opts.tempSourceCsv, type, function (error, data) {
//                //  d3.csv("amzn.csv", type, function(error, data) {
//                if (error) throw error;


//              data = data.filter(function (i) { return i.date.getMonth() >= Math.floor(Math.random() * 12); });
//                var clipPath = api.clipPath();
//                var clipRect = clipPath(svg, width, height);
//                api.clipRect = clipRect;
//               // svg = clipPath.svg();
//  /*              svg.append("defs").append("clipPath")
//                    .attr("id", "clip")
//                  .append("rect")
//                    .attr("width", width)
//                    .attr("height", height);
//*/

//                x.domain(d3.extent(data, function (d) { return d.date; }));
//                y.domain([0, d3.max(data, function (d) { return d.price; })]);
//                x2.domain(x.domain());
//                y2.domain(y.domain());

//                /**** this provides the line graph in the focus view
//                *
//                  focus.append("path")
//                  .datum(data)
//                    .attr("class","line")
//                    .attr("clip-path", "url('#clip')")
//                    .attr("d",line)
//                    .attr("stroke-linecap","round")
//                    .attr("stroke-width", "1")
//                   .attr('stroke', '#723')
//                   .attr('stroke-opacity', 0.5);
//                   ***/


//                /***
//                *main or focus timline view   .attr("clip-path", "url('/~powersparks/bz.html#clip')")
//                  focus.append("path")
//                      .datum(data)
//                      .attr("class", "area")
//                      .attr("clip-path", "url('#clip')")
//                      .attr("d", area);
//                **/

//                focus.append("g")
//                    .attr("class", "axis axis--x")
//                    .attr("transform", "translate(0," + height + ")")
//                    .call(xAxis);

//                focus.append("g")
//                    .attr("class", "axis axis--y")
//                    .call(yAxis);

//                /****for the lower or context timeline and brush *
//                  context.append("path")
//                      .datum(data)
//                      .attr("class", "area")
//                      .attr("d", area2);
//                **/

//                /**
//                 * this focus select doesn't use the datum approach
//                 * which may be a problem...
//                 * TODO: figure out how to bind the datum
//                 */
//                focus.selectAll("circle")
//                  .data(data).enter().append("circle")
//                  .attr("clip-path", "url('#clip')")
//                  .attr("d", line)
//                  .attr("class", "dot")
//                  .attr("r", 3.5)
//                  .attr("opacity", 0.7)
//                  .style("fill", "steelblue");

//                focus.selectAll("line")
//                  .data(data).enter().append("line")
//                  .attr("clip-path", "url('#clip')")
//                  .attr("d", line)
//                  .attr("class", "line")
//                  .attr("stroke-linecap", "round")
//                  .attr("stroke-width", "1")
//                 .attr('stroke', "steelblue")
//                 .attr('stroke-opacity', 0.5);
//                //.attr("r", 3.5)
//                //    .attr("opacity", 0.7)
//                //    .style("fill",  "steelblue");

//                /**
//                 * brush get's drawn by user
//                 */
//                var focusBrushSlider = focus.append("g")
//                     .attr("class", "brush focus-brush")

//                     .style("cursor", "pointer")
//                     .call(focusBrush)
//                     .call(focusBrush.move, [width - Math.floor(width / 2) - (width / 5), width - Math.floor(width / 2) + (width / 5)]);
//                /*  focusBrushSlider.on({
//                      "mouseover": function (d) {
//                          d3.select(this).style("cursor", "pointer");
//                      },
//                      "mouseout": function (d) {
//                          d3.select(this).style("cursor", "")
//                      }
//                  });
//                  */
//                //console.info(x.range());
//                /**
//                 * context time line setup:
//                 */
//                context.append("g")
//                        .attr("class", "axis axis--y")
//                        .call(yAxis2);

//                context.append("g")
//                     .attr("class", "axis axis--x")
//                     .attr("transform", "translate(0," + height2 + ")")
//                     .call(xAxis2)


//                context.append("g")
//                     .attr("class", "brush")
//                    // .attr("stroke-width", "1")
//                    //.attr('stroke', '#000')
//                    //.attr('stroke-opacity', 1)
//                //.attr("border", "solid 1px #000")
//                     .call(brush)
//                     .call(brush.move, x.range());

//                focus.append("rect")
//                    .attr("class", "zoom")
//                    .attr("width", width)
//                    .attr("height", height)
//                    .attr("transform", "translate(" + 0  + "," + Math.floor(margin.top/2) + ")")
//                  //.attr("transform", "translate(" + margin.left  + "," + margin.top + ")")

//                    .call(zoom);
////[width - Math.floor(width / 2) - (width / 5), width - Math.floor(width / 2) + (width / 5)]
//            });