(function ($, global) {

    var model = {
        update: function (context, options) {
            return $.telligent.evolution.post({
                url: context.updateUrl,
                dataType: 'json',
                data: options
            });
        },
        del: function (mapBookId) {
            return $.telligent.evolution.del({
                url: $.telligent.evolution.site.getBaseUrl() + 'api.ashx/v2/mapbook/{mapBookId}.json',
                data: {
                    mapBookId: mapBookId
                }
            });
        },
        searchGroups: function (context, textbox, searchText) {
            if (searchText && searchText.length >= 2) {

                textbox.glowLookUpTextBox('updateSuggestions', [
					textbox.glowLookUpTextBox('createLookUp', '', spinner, spinner, false)
                ]);

                $.telligent.evolution.get({
                    url: $.telligent.evolution.site.getBaseUrl() + 'api.ashx/v2/groups.json',
                    data: {
                        GroupNameFilter: searchText,
                        Permission: 'Group_ReadGroup'
                    },
                    success: function (response) {
                        if (response && response.Groups && response.Groups.length >= 1) {
                            textbox.glowLookUpTextBox('updateSuggestions',
								$.map(response.Groups, function (group, i) {
								    return textbox.glowLookUpTextBox('createLookUp', group.Id, group.Name, group.Name, true);
								}));
                        } else {
                            textbox.glowLookUpTextBox('updateSuggestions', [
								textbox.glowLookUpTextBox('createLookUp', '', context.text.noGroupsMatch, context.text.noGroupsMatch, false)
                            ]);
                        }
                    }
                });
            }
        },
        searchUsers: function (context, textbox, searchText) {
            window.clearTimeout(context.addMemberUserNameTimeout);
            if (searchText && searchText.length >= 2) {
                textbox.glowLookUpTextBox('updateSuggestions', [textbox.glowLookUpTextBox('createLookUp', '', spinner, spinner, false)]);
                context.addMemberUserNameTimeout = window.setTimeout(function () {
                    $.telligent.evolution.get({
                        url: context.lookupUsersUrl,
                        data: { w_SearchText: searchText },
                        success: function (response) {
                            if (response && response.matches.length > 1) {
                                var suggestions = [];
                                for (var i = 0; i < response.matches.length; i++) {
                                    var item = response.matches[i];
                                    if (item && item.userId) {
                                        suggestions.push(textbox.glowLookUpTextBox('createLookUp', 'user:' + item.userId, item.title, item.title, true));
                                    }
                                }

                                textbox.glowLookUpTextBox('updateSuggestions', suggestions);
                            }
                            else
                                textbox.glowLookUpTextBox('updateSuggestions', [textbox.glowLookUpTextBox('createLookUp', '', context.text.noUsersMatch, context.text.noUsersMatch, false)]);
                        }
                    });
                }, 500);
            }
        }

    };


    var api = {
        register: function (context) {
            /**header**/
            var header = $($.telligent.evolution.template.compile(context.saveTemplateId)({}));
            var saveButton = header.find('a.save');
            $.telligent.evolution.administration.header(header);
            /**inputs
             * <li class="field-item">
                <label class="field-item-name"></label>
                  <span class="field-item-description"></span>
                  <span class="field-item-input">
	                  <input type="text" value="" id="" name="" />
                  </span>
                  <span class="field-item-validation" style="display:none;"></span>
                </li>
             **/
            function addColorInput(options) {
                options = options ? options : {};
                var opt = {};
                opt.itemId = options.itemId ? options.itemId : null;
                opt.itemElement = options.itemElement ? options.itemElement : "input";
                opt.itemType = options.itemType ? options.itemType : "color";
                opt.itemName = options.itemName ? options.itemName : "New Name";
                opt.itemDescription = options.itemDescription ? options.itemDescription : "Name description.";
                opt.itemPlaceholder = options.itemPlaceholder ? options.itemPlaceholder : "add text";
                
                opt.itemValue = options.itemValue ? options.itemValue : null;

                //console.info(options);
                var fieldItemTemplate = $($.telligent.evolution.template.compile(context.fieldItemTemplateId)({}));
                fieldItemTemplate.find(opt.itemElement)
				.glowColorSelector('val', opt.itemValue)
					.attr("id", opt.itemId)
                    .attr("type", opt.itemType)
					.attr("placeholder", opt.itemPlaceholder)
                    
                fieldItemTemplate.find(".field-item-name")
                     .html(opt.itemName);
                fieldItemTemplate.find('.field-item-description')
                    .html(opt.itemDescription);
                return fieldItemTemplate;
            };
            function addTextInput(options) {
                options = options ? options : {};
                var opt = {};
                opt.itemId = options.itemId ? options.itemId : null;
                opt.itemElement = options.itemElement ? options.itemElement : "input";
                opt.itemType = options.itemType ? options.itemType : "text";
                opt.itemName = options.itemName ? options.itemName : "New Name";
                opt.itemDescription = options.itemDescription ? options.itemDescription : "Name description.";
                opt.itemPlaceholder = options.itemPlaceholder ? options.itemPlaceholder : "add text";
                opt.itemValue = options.itemValue ? options.itemValue : null;
               // console.info(options);
                var fieldItemTemplate = $($.telligent.evolution.template.compile(context.fieldItemTemplateId)({}));
                fieldItemTemplate.find(opt.itemElement)
					.attr("id", opt.itemId)
                    .attr("type", opt.itemType)
					.attr("placeholder", opt.itemPlaceholder)
                    .val(opt.itemValue);
                fieldItemTemplate.find(".field-item-name")
                     .html(opt.itemName);
                fieldItemTemplate.find('.field-item-description')
                    .html(opt.itemDescription);

                return fieldItemTemplate;
            };
            var options = {
                itemId: context.inputs.nameId,
                itemElement: "input",
                itemType: "text",
                itemName: context.text.nameLabel,
                itemDescription: context.text.nameLabel_Desc,
                itemPlaceholder: context.text.requiredText,
                itemValue: context.values.mapBookName
            };
            var clrOptions = {
                itemId: context.inputs.nameId + "_clr",
                itemElement: "input",
                itemType: "color",
                itemName: context.text.nameLabel,
                itemDescription: context.text.nameLabel_Desc,
                itemPlaceholder: context.text.requiredText,
                itemValue: '#ff66ff'
            };
            //prepend will add content order, last is first
            $(context.fieldInputListId ,//+ " li:first",
                $.telligent.evolution.administration.panelWrapper())
                .prepend(addColorInput(clrOptions))
                 .prepend(addTextInput())
				.prepend(addTextInput(options));
             
             
            api.registerViewIdentifiers();
           // api.registerSaveBtn(saveButton, context);
//            api.registerViewDelete(context);
           // api.registerInputsGroup(context);

        },
        registerSaveBtn: function (saveButton, context) {
            saveButton.evolutionValidation({
                onValidated: function (isValid, buttonClicked, c) {
                },
                onSuccessfulClick: function (e) {
                    //var groupId = parseInt($(context.inputs.group).val());
                    var continueSave = true;
                       
                    var updateOptions = {
                        id: context.mapbookId,
                        name: $(context.inputs.name).val(),
                        description: $(context.inputs.description).val(),
                        address: appKey,
                        enable: enabled,
                        authors: userIds.join(),
                        enableAggBugs: $(context.inputs.enableAggBugs).is(':checked'),
                        includeInSiteMap: $(context.inputs.includeInSiteMap).is(':checked'),
                        includeInAggregate: $(context.inputs.includeInAggregate).is(':checked'),
                        groupId: groupId,
                        enableContact: enableContact
                    };

                    if (context.mailGatewayEnabled) {
                        updateOptions.mailingListEnabled = $(context.inputs.mailingListEnabled).is(':checked');
                    }

                    if (enableContact)
                        updateOptions.contactEmailAddress = contactEmailAddress;

                    if (continueSave) {
                        model.update(context, updateOptions).done(function (response) {
                            $.telligent.evolution.notifications.show(context.text.updateSuccess, { type: 'success' });

                            if ((context.groupId != -1 && groupId > 0 && context.groupId != groupId) || appKey != context.applicationKey) {
                                window.location.href = response.redirectUrl;
                            }
                            else if (!enabled && context.redirect) {
                                window.location.href = context.groupRedirect;
                            } else if (!context.mailingListEnabled && updateOptions.mailingListEnabled) {
                                // if the mailing list was just enabled, refresh panel to load and show email address
                                $.telligent.evolution.administration.refresh();
                            }
                        });
                    }
                }
            })
        .evolutionValidation('addField', $(context.inputs.nameId), {
            required: true, maxlength: 256
        }, $(context.inputs.nameId).closest('.field-item').find('.field-item-validation'), null)
        .evolutionValidation('addField', $(context.inputs.nameId), {
            required: true,
            pattern: /^.*[A-Za-z0-9]+.*$/,
            messages: {
                pattern: context.text.namePatternMessage
            }
        }, $(context.inputs.name).closest('.field-item').find('.field-item-validation'), null)
        .evolutionValidation('addField', $(context.inputs.descriptionId), { maxlength: 256 }, $(context.inputs.descriptionId).closest('.field-item').find('.field-item-validation'), null);
       
       
        },
        registerInputsGroup: function (context) {
            context.inputs.groupId = $(context.inputs.groupId)
           .glowLookUpTextBox({
               delimiter: ',',
               allowDuplicates: true,
               maxValues: 1,
               onGetLookUps: function (tb, searchText) {
                   model.searchGroups(context, tb, searchText);
               },
               emptyHtml: '',
               selectedLookUpsHtml: [],
               deleteImageUrl: ''
           });
        },
        registerViewIdentifiers: function () {

            var viewIdentifiers = $('a.viewidentifiers', $.telligent.evolution.administration.panelWrapper());
            viewIdentifiers.click(function () {
                $('li.identifiers', $.telligent.evolution.administration.panelWrapper()).each(function () {
                    $(this).toggle();
                });

                return false;
            });
        },
        registerViewDelete: function (context) {
            $.telligent.evolution.messaging.subscribe('contextual-delete', $.telligent.evolution.administration.panelNameSpace(), function () {
                if (confirm(context.text.deleteConfirm)) {
                    model.del(context.MapBook.Id).done(function () {
                        window.location.href = context.groupRedirect;
                    });
                }
            });
        },
        registerSearchGroups: function (context) { },
        registerSearchUsers: function (context) { },
        /**
* LayerOptions provides layer options
* to use just the object implement usage:
* @example var options = new $.apan.maps.LayerOptions().title("change title name").order(2);
* @example options() //returns the modified object
* * @returns {function}  
*/
        LayerOptions: function ()
        {
         
            var _map = null,
                _layerId = null
            _id = null,
            _type = null,
            _title = "",
            _order = 0,
            _url = "",
            _isHidden = false,
            _isEnabled = false,
            _isInternal = false,
            _collapsed = true;
            _setMapByDefault = false;
            /**
             * @param {Layer} layer
             * @returns {object} 
             */
            function LayerOptions(layer)
            {
           
                if (!arguments.length )
                {
                    return { 
                        map: _map,
                        id: _id,
                        layerId: _layerId,
                        type: _type,
                        title: _title,
                        order: _order,
                        url: _url,
                        isHidden: _isHidden,
                        isEnabled: _isEnabled,
                        isInternal: _isInternal,
                        collapsed: _collapsed,
                        setMapByDefault: _setMapByDefault
                    };
                }
                _id = layer.id;
                _layerId = layer.id;
                _map = layer.getMap();
                _type = layer.getType();
                _title = layer.getTitle();
                _order = layer.getOrder();
                _url = layer.getUrl();
                _isHidden = layer.isHidden();
                _isInternal = layer.isInternal();
            
            };
            /**
             * Accessors, used in LayerOptions Class (like get; set;) and includes chaining
             * @param {type} value
             * @returns {type} 
             */
            LayerOptions.id = function (value) { if (!arguments.length) { return _id; } _id = value; _layerId = _id; return LayerOptions; };
            LayerOptions.layerId = function (value) { if (!arguments.length) { return _layerId; } _layerId = value; _id = _layerId; return LayerOptions; };
            LayerOptions.map = function (value) { if (!arguments.length) { return _map; } _map = value; return LayerOptions; };
            LayerOptions.type = function (value) { if (!arguments.length) { return _type; } _type = value; return LayerOptions };
            LayerOptions.title = function (value) { if (!arguments.length) { return _title; } _title = value; return LayerOptions };
            LayerOptions.order = function (value) { if (!arguments.length) { return _order; } _order = value; return LayerOptions };
            LayerOptions.url = function (value) { if (!arguments.length) { return _url; } _url = value; return LayerOptions };
            LayerOptions.isHidden = function (value) { if (!arguments.length) { return _isHidden; } _isHidden = value; _isEnabled = _isHidden; return LayerOptions };
            LayerOptions.isEnabled = function (value) { if (!arguments.length) { return _isEnabled; } _isEnabled = value; _isHidden = _isEnabled; return LayerOptions };
            LayerOptions.isInternal = function (value) { if (!arguments.length) { return _isInternal; } _isInternal = value; return LayerOptions };
            LayerOptions.collapsed = function (value) { if (!arguments.length) { return _collapsed; } _collapsed = value; return LayerOptions };
            LayerOptions.setMapByDefault = function (value) { if (!arguments.length) { return _setMapByDefault; } _setMapByDefault = value; return LayerOptions; };

            return LayerOptions;
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

