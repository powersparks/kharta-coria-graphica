/// <reference path="d3.js" />
/// <reference path="C:\Program Files\Telligent\9.2\Utility\JQuery\jquery.min.js" />
/// <reference path="C:\Program Files\Telligent\9.2\Utility\JQuery\evolution\telligent.evolution.js" />

/*** <reference path="https://d3js.org/d3.v4.js" />
 <reference path="https://harleyparks.com/Utility/JQuery/jquery.min.js" />
 <reference path="https://harleyparks.com/Utility/JQuery/evolution/telligent.evolution.js" />
 <reference path="https://maps.google.com/maps/api/js?sensor=false&signed_in=true&client=gme-disaapan" />
 */
 

$ = $ || {};
$.apan = $.apan || {};
$.apan.maps = $.apan.maps || {};
$.apan.maps.Layer = $.apan.maps.Layer || {};
$.apan.maps.AgsLayer = $.apan.maps.AgsLayer || {};
$.apan.maps.AgsLayer.get = function () { if (arguments.length > 0) { return arguments[0] } };
searchApiOptions = {
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
    _pageIndex: _pageIndex
};
                
 
            