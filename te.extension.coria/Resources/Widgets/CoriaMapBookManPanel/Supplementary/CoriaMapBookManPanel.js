(function ($, global) {

    var model = {
        update: function (context, options) {
            return $.telligent.evolution.post({
                url: context.updateUrl,
                dataType: 'json',
                data: options
            });
        }
    };


    var api = {
        register: function (context) {
            /****/
            var header = $($.telligent.evolution.template.compile(context.saveTemplateId)({}));
            var saveButton = header.find('a.save');
            $.telligent.evolution.administration.header(header);




        },
        viewIdentifiers: function () {

            var viewidentifiers = $('a.viewidentifiers', $.telligent.evolution.administration.panelWrapper());
            viewidentifiers.click(function () {
                $('li.identifiers', $.telligent.evolution.administration.panelWrapper()).each(function () {
                    $(this).toggle();
                });

                return false;
            });
        }
    };


    /** namespace **/
    $.coria = $.coria || {};
    $.coria.mapbooks = $.coria.mapbooks || {};
    $.coria.mapbooks.ui = api;

})(jQuery, window);

/****
 *   var maplayerdef = context.maplayerdef;
      var mapsettings = context.mapsettings;
      var panelId = context.panelId;
      var tabsId = context.tabsId;

      $('.' + 'filter-option').click(function(e){
          e.preventDefault();
           $('.filter-option.selected').removeClass('selected');
              $(this).addClass('selected');
              var filter = $(this).data("filter");
              console.info(filter);
               
          
      });
       ****/
