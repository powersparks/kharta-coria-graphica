///<reference path="http://d3js.org/d3.v4.min.js"/>
///<reference path="C:\Program Files\Telligent\9.2\Utility\JQuery\evolution\telligent.evolution.js"/>
 
(function ($) {
    var context = {
        coriaChevronDownSvgId: "coria-chevron-down-svg-id",
        newChartDivId: "newChartDivId",
        containerId: "containerId",
        newLegendDivId: "newLegendDivId",
        coriaLegendId: "coria-legend-id",
        containerTemplateId: "containerTemplateId",
        widgetId: '$core_v2_widget.WrapperElementId'
    };
    var _widgetId = null;
    var api = {
        register: function (context) {
            api.widgetIds().push(context.widgetId);
            api.widgetIds[context.widgetId] = api.widgetId(context.widgetId);
        },
        widgetIds: function (widgetIdCallback) { if (!arguments.length) { return new Object(); return widgetIdCallback;} },
        widgetId: function (value) { if (!arguments.length) { return _widgetId; } _widgetId = value; return api; },
        register2: function (context) {

            var coriaChevronDownSvgId = context.coriaChevronDownSvgId;
            d3.select("#" + context.containerId)
            .attr("width", 20)
            .attr("height", 20);
            d3.select("#" + context.containerId).append("circle")
            .attr('cx', 10)
            .attr('cy', 10)
            .attr('r', 10);

            var $testTemplate = $($.telligent.evolution.template.compile(context.coriaChevronDownSvgId)({}));
            var $legendTemplate = $($.telligent.evolution.template.compile(context.coriaLegendId)({}));
            //stopped here.. need to duplicate this using d3
            var legTemp = d3.selectAll($testTemplate.toArray());
            var legendNode = d3.selectAll($legendTemplate.toArray());

            d3.select("#" + context.newLegendDivId).node()
                 .append(legendNode.node());

            //console.info(legTemp.node());
            d3.select("#" + context.newLegendDivId).node()
              .append(legTemp.node());
            // $("#" + context.newLegendDivId)
            //.append(legendTemplate.toString());
            // coriaLegendId


        }
    };
    // 1) set up a reference to D3
    // 2) create jquery module
    // 3) temporary context, use it to keep track of the variables
    var context = {
        coriaChevronDownSvgId: "coria-chevron-down-svg-id",
        newLegendDivId: "newChartDivId"
    };
    function Legend() {
        //list of variables 
        var legendDivId = context.newLegendDivId;
        var svgId = context.coriaChevronDownSvgId;
        // select the element
        var legend = d3.select('#' + legendDivId);
        return legend;
    };
    var addSourcesLayersLegends = function (SourcesLayersLegends) {

        var gcntlBtn = "gcntl-btn";
        var coriaNavContent = "coria-nav-content";
        var coriaNavBarBtns = "coria-nav-bar-btns";
        var gcntlBtnAbstracts = "gcntl-btnAbstracts";
        var gcntlBtnLayers = "gcntl-btnLayers";
        var gcntlBtnLegends = "gcntl-btnLegends";
        var gcntlBtnDetails = "gcntl-btnDetails";
        var coriaAbstractPane = "coria-abstract-pane";
        var coriaLayerPane = "coria-layer-pane";
        var coriaLegendPane = "coria-legend-pane";
        var coriaDetailPane = "coria-detail-pane";
        var coriaMapPanels = "coria-map-panels";
        var isCheveronUp = function (coriaNavBarBtns) {
            if ($('.' + coriaNavBarBtns).children(".coria-chevron-up").hasClass("coria-chevron-up")) {
                return true;
            } else {
                return false;
            }

        };

        //<!--body onload=addSourcesLayersLegends("SourcesLayersLegends")-->

        var toggleMapPanels = function (gcntlBtnNameId, coriaNavNamePane, coriaNavBarBtns, gcntlBtn, coriaMapPanels) {
            if ($('#' + gcntlBtnNameId).hasClass("gcntl-btn-selected")) {

                // $('.' + coriaNavBarBtns).click();
                if (!isCheveronUp(coriaNavBarBtns)) {
                    $('.' + gcntlBtn).removeClass("gcntl-btn-selected");
                    $('.' + coriaNavBarBtns).click();
                    $('.' + coriaNavNamePane).hide();
                } else {
                    $('.' + coriaNavBarBtns).click();
                }

            } else {
                $('.' + gcntlBtn).removeClass("gcntl-btn-selected");

                $('.' + coriaMapPanels).hide();

                $('#' + gcntlBtnNameId).addClass("gcntl-btn-selected");

                if (isCheveronUp(coriaNavBarBtns)) {
                    $('.' + coriaNavBarBtns).click();
                }

                $('.' + coriaNavNamePane).show();



            }
        };
        $('#' + gcntlBtnAbstracts).click(function () {
            toggleMapPanels(gcntlBtnAbstracts, coriaAbstractPane, coriaNavBarBtns, gcntlBtn, coriaMapPanels);


        });
        $('#' + gcntlBtnLayers).click(function () {
            toggleMapPanels(gcntlBtnLayers, coriaLayerPane, coriaNavBarBtns, gcntlBtn, coriaMapPanels);

        });
        $('#' + gcntlBtnLegends).click(function () {
            toggleMapPanels(gcntlBtnLegends, coriaLegendPane, coriaNavBarBtns, gcntlBtn, coriaMapPanels);

        });
        $('#' + gcntlBtnDetails).click(function () {
            $('#' + gcntlBtnDetails).show();
            toggleMapPanels(gcntlBtnDetails, coriaDetailPane, coriaNavBarBtns, gcntlBtn, coriaMapPanels);

        });

        $('.' + coriaNavBarBtns).click(function () {

            if ($(this).children(".coria-chevron-up").hasClass("coria-chevron-up")) {
                $(this).children(".coria-chevron-up").addClass("coria-chevron-down");
                $(this).children(".coria-chevron-up").removeClass("coria-chevron-up");
                $('.' + coriaNavContent).show();

            } else if ($(this).children(".coria-chevron-down").hasClass("coria-chevron-down")) {
                $(this).children(".coria-chevron-down").addClass("coria-chevron-up");
                $(this).children(".coria-chevron-down").removeClass("coria-chevron-down");
                $('.' + coriaNavContent).hide();
                //$('.' + gcntlBtn).removeClass("gcntl-btn-selected");
            };
        });
    };
    $.coria = $.coria ? $.coria : {};
    $.coria.legend = api;
})(jQuery);