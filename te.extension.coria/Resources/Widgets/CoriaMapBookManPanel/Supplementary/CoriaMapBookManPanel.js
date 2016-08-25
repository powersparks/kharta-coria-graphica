(function ($, global) {

     var panelFunc = {
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
             //see: source line#23488
             saveButton.evolutionValidation({
                 //see: method in api line#23133 & #23498 
                 onValidated: function (validationSuccessful, submitButtonClicked, failedValidationContext) {
                     console.info("onValidated: function (validationSuccessful, submitButtonClicked, failedValidationContext)");
                     console.info(validationSuccessful);
                     console.info(bsubmitButtonClicked);
                     console.info(failedValidationContext);
                 },
                 onSuccessfulClick: function (evt) {
                     /***confirm user set options, set and check input values***/
                     var continueSave = true;
                     try {
                        var updateOptions = {
                                                 id     : '',
                                                 item1  : '',
                                                 item2  : ''
                                             };
                     } catch (e) {
                         continueSave = false;
                         //add notification and logging 
                         $.telligent.evolution.notifications.show(e.message, { type: 'error' });
                     }
                     
                     //if "updateOptions"  and if continue save is true
                     if (continueSave) {
                        panelFunc.update(context, updateOptions).done(function (response) {
                             $.telligent.evolution.notifications.show(context.text.updateSuccess, { type: 'success' });
                            //redirect if needed

                         });
                     }

                 }
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
/**** 
jQuery(function (j) {
   j.coria.mapbooks.ui.register({
       maplayerdef: "maplayerdef",
       mapsettings: "mapsettings",
       panelId: "panelId",
       tabsId: "tabs"
   });
});
*****/
