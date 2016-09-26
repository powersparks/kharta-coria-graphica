/// <reference path="https://maps.google.com/maps/api/js?v=3.23&client=gme-disaapan" />
/// <reference path="https://api.tiles.mapbox.com/mapbox-gl-js/v0.20.1/mapbox-gl.js" />

(function ($) {
    if (typeof $.coria === 'undefined') { $.coria = {}; }
    if (typeof $.coria.map === 'undefined') { $.coria.map = {}; }

    function mapthumbnail(inMapDivId, containerId, outCanvasId) {
        html2canvas($('#' + inMapDivId), {
            useCORS: true,
            allowTaint: true,
            onrendered: function (outCanvasId) {
                $('#' + containerId).append(outCanvasId);
            }
        });
        //or 
        return html2canvas;
    };
     
    var api = {

        register: function (context) {
            $.coria.map.WidgetIds(context);
            var mapOptions = {
                mapCanvasId: context.mapCanvasId,
                lat: context.mapCenterLat,
                lng: context.mapCenterLng,
                zoom: context.mapZoomLevel,
                height: context.height,
                width: context.width
            };

            $('#' + context.mapCanvasId).css('width', context.width);
            $('#' + context.mapCanvasId).css('height', context.height);
            switch (context.mapApi) {
                case "Base": api.initBase(mapOptions); break;
                case "MapBox": api.initMapBox(mapOptions); break;
                case "GoogleMaps": api.initGoogleMaps(mapOptions); break;
                case "ArcGis": api.initArcGis(mapOptions); break;
                case "ArcGis4": api.initArcGis4(mapOptions); break;
                default: console.debug("wrong api: " + context.mapApi); break;
            }

        },
        initBase: function (options) {

            $('#' + options.mapCanvasId)
            .append("Map Options: " + JSON.stringify(options));
        },
        initMapBox: function (options) {

            var map = new mapboxgl.Map({
                container: options.mapCanvasId,
                style: 'mapbox://styles/mapbox/streets-v9', //stylesheet location
                center: [options.lng, options.lat], // starting position
                zoom: options.zoom // starting zoom
            });

        },
        initGoogleMaps: function (options) {

            var map = new google.maps.Map($('#' + options.mapCanvasId)[0], mapOptions());
            function mapOptions() {

                return {

                    center: new google.maps.LatLng(options.lat, options.lng),
                    zoom: options.zoom,
                    zoomControl: true,
                    zoomControlOptions: {
                        style: google.maps.ZoomControlStyle.SMALL,
                        position: google.maps.ControlPosition.RIGHT_BOTTOM
                    },
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    disableDefaultUI: false,
                    panControl: true,
                    mapTypeControl: true,
                    scaleControl: true,
                    streetViewControl: true,
                    overviewMapControl: true,
                    overviewMapControlOptions: {
                        opened: false
                    }
                };
            };
        },
        initArcGis: function (options) {

            require(["esri/map", "dojo/domReady!"], function (Map) {
                var map = new Map(options.mapCanvasId, {
                    center: [options.lng, options.lat],
                    zoom: options.zoom,
                    basemap: "topo"
                });
            });


        },
        initArcGis4: function (options) {
            require([
                "esri/Map",
                "esri/views/MapView",
                "dojo/domReady!"
            ], function (Map, MapView) {
                var map = new Map({
                    basemap: "streets"
                });
                var view = new MapView({
                    container: options.mapCanvasId,  // Reference to the DOM node that will contain the view
                    map: map,
                    zoom: options.zoom, // References the map object created in step 3
                    center: [options.lng, options.lat]

                });
            });

        },
        WidgetIds: new _widgetIds()
    };

    $.coria.map = api;

    function _widgetIds() {
        var _id = null;
        var _ids = []
        var WidgetIds = function (context) {
            if(!arguments.length){
                return {
                    lastId: _id,
                    ids: _ids
                };
            }
            _id = context.widgetId;
            _ids.push(context.widgetId);
            
        };
        WidgetIds.lastId = function (value) { if (!arguments.length) return _id; _id = value; return WidgetIds; };
        WidgetIds.ids = function (value) { if (!arguments.length) return _ids; _ids.push(value); return WidgetIds; };
        return WidgetIds;
    }

})(jQuery);