﻿lazyloaderController = function () {
    var //Support  
        wireListeners = function() {
            pubsub.subscribe('action.plugin.lazyload', function(subject, payload) { fetch(payload); }); 
        },   
        retrievePlugin = function(key) {   
            var currentData = data.current();
            $.ajax({
                url : util.replaceTokens(data.currentMetadata().resources.glimpse_tab, { 'requestId' : currentData.requestId, 'pluginKey' : key }),
                type : 'GET',
                contentType : 'application/json',
                success : function (result) {
                    var itemData = currentData.data[key];
                    itemData.data = result;  
                    itemData.isLazy = false;

                    pubsub.publishAsync('action.tab.select', key);
                }
            });
        },
        
        //Main 
        fetch = function (key) {
            retrievePlugin(key); 
        }, 
        init = function () {
            wireListeners();
        };
    
    init(); 
} ()