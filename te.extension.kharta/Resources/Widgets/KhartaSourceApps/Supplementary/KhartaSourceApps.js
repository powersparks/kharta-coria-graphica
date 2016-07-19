(function ($) {

    
    $.kharta.source = {
        register: function (context) {
            var map = new mapboxgl.Map({
                container: context.containerMapId,//'map', // container id
                style: 'mapbox://styles/mapbox/streets-v9', //stylesheet location
                center: [-74.50, 40], // starting position
                zoom: 9 // starting zoom
            });
        }
    };
})(jQuery);