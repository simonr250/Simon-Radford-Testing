/****
imagedir
-- set this to the location of the images that you will be using
-- for your ajax loader and your ascending/descending hints 
-- i.e. on Windows XP, this default corresponds to http://localhost/MvcContribJQuery/Content/Images
-- LEADING slash only
*****/
var imagedir = "/Content/GridImg";
/****
applicationname
-- set this to your application subdirectory
-- if you do not have an application subdirectory, leave completely blank
-- i.e. on Windows XP, this default corresponds to http://localhost/MvcContribJQuery
-- LEADING slash only
*****/
var applicationname = "";
var mvcgrids = {};

function fixGrids(griddiv) {
    // Fix Firefox 3 bug where scrolling table rows fill tbody height if thereare not enough rows to scroll.
    if (navigator.userAgent.indexOf("Firefox/3") > -1) {
        var tbodies = $(griddiv + " tbody");
        tbodies.each(function() {
            var trows = $(this).children("tr");
            var tbodyHeight = $(this).height();
            var rowsHeight = 0;
            trows.each(function() {
                rowsHeight += $(this).height();
                if (rowsHeight >= tbodyHeight) return false;
            });
            if (rowsHeight <= (tbodyHeight + 1)) $(this).height("auto");
        });
    }
    else {
        if (navigator.userAgent.indexOf("MSIE") > -1) {
            var tbodies = $(griddiv + " tbody");
            tbodies.each(function() {
                $(this).height("auto");
            });
        }
    }
}

function UpdateGrid(griddiv, controller, gridview, col, page, searchword) {
    // when sorting, page is undefined or the same
    // when paging, column is the same
    var mvcgridvals = mvcgrids[griddiv];
    if (mvcgridvals == null) {
        mvcgridvals = {};
        mvcgridvals["currentpage"] = 1;

		//SR - If the grid is for top rated products then sort descending by default
        if (griddiv == "#top_rated_product_grid")
        { mvcgridvals["direction"] = "DESC"; }
        else { mvcgridvals["direction"] = "ASC"; }
		//----------------------------------------------
        mvcgridvals["sortcolumn"] = col;
    }
    else {
        if (page !== "" && page !== undefined && mvcgridvals["currentpage"] !== page) {
            // changing page
            mvcgridvals["currentpage"] = page;
        }
        else {
            // page being the same means we're sorting
            if (mvcgridvals["sortcolumn"] == col) {
                // same column means we need to switch ascending and descending
                if (mvcgridvals["direction"] == "ASC") {
                    mvcgridvals["direction"] = "DESC";
                }
                else {
                    mvcgridvals["direction"] = "ASC";
                }
            }
            else {
                // different column means we're starting with ascending
                mvcgridvals["direction"] = "ASC";
            }
            mvcgridvals["sortcolumn"] = col;
        }
    }
    // persist grid variables
    mvcgrids[griddiv] = mvcgridvals;
    // correcting for trailing slash -- no VirtualPathUtility here.
    var myhost = window.location.protocol + "//" + window.location.host
    var absoluteapp = myhost + applicationname;
    $("#loading").html("<img src=\"" + absoluteapp + imagedir + "/ajax-loader.gif\" />");
    $.ajax({
        type: "POST",
        url: absoluteapp + controller,
        data: ({ ColumnName: mvcgridvals["sortcolumn"], PageNum: mvcgridvals["currentpage"], Controller: controller, Griddiv: griddiv, GridView: gridview, Direction: mvcgridvals["direction"], SearchWord: searchword }),
        success: function(msg) {
            $(griddiv).html(msg);
            $(griddiv).mvcajax(controller, gridview, searchword, { databinding: true });
            fixGrids(griddiv);
            $("#" + mvcgridvals["sortcolumn"] + "_Sort").html("&nbsp;&nbsp;<img src='" + absoluteapp + imagedir + "/" + mvcgridvals["direction"] + ".gif' />");
            $("#loading").html("&nbsp;");
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            alert("Data Failed to Sort: " + XMLHttpRequest.statusText);
            $("#loading").html("&nbsp;");
        }
    });
}

(function ($) {
	$.fn.extend({
		mvcajax: function (controller, gridview, searchword, options) {
			var defaults = {
				defaultsort: '',
				databinding: false
			};

			var settings = $.extend(defaults, options);

			// which element are we applying this to?
			var selector = $(this).selector;

			if (!settings.databinding) {
				UpdateGrid(selector, controller, gridview, settings.defaultsort, undefined, searchword);
				return false;
			}

			// helper function to parse querystring
			getPage = function (path) {
				var pagenum = 0;
				$.each(path.split("?")[1].split("&"),
                    function (key, val) {
                    	var params = val.split("=");
                    	pagenum = params[1];
                    	return (params[0] !== "page");
                    }
                );
				return pagenum;
			};

			return this.each(function () {
				// step 1 -- manipulate pager
				// step 1.a -- retrieve pager links
				var anchors = $(selector + " .pagination").children(".paginationRight").children("a");
				var pagenum;
				var updatelink = "javascript:UpdateGrid('#0', '#1', '#2', '#3', '#4', '#5');";
				updatelink = updatelink
                        .replace("#0", selector)
                        .replace("#1", controller)
                        .replace("#2", gridview)
						.replace("#5", searchword);
				anchors.each(function () {
					// step 1.b -- for each pager link, retrieve page linked to
					pagenum = getPage($(this).attr("href"));
					// step 1.c -- change page link to match controller 
					$(this).attr("href",
                      updatelink
                        .replace("#3", '')
                        .replace("#4", pagenum)
                    );
				});
				// step 2 -- manipulate header
				// step 2.a -- retrieve header items
				var header = $(selector + " .grid").children("thead").children("tr").children("th");
				var anchor;
				var sortcolumn;
				header.each(function () {
					// step 2.b -- manage links to javascript function
					anchor = $(this).children("a");
					// if there's no pre-existing link on the header 
					// (from a header override, maybe), we create a new one
					if (anchor.length == 0) {
						$(this).wrapInner("<a></a>");
					}
					anchor = $(this).children("a");
					sortcolumn = $(this).attr("sortcolumn");
					anchor.attr("href",
                      updatelink
                        .replace("#3", sortcolumn)
                        .replace("#4", '')
                    );
					// step 3 -- add span for ASC/DESC image
					$(this).append("<span id='" + sortcolumn + "_Sort'></span>");
				});
				// step 3 -- manipulate page dropdown
				// step 3.a -- retrieve page dropdown items
				var dropdown = $(selector + " .pagedropdown").children("select");
				dropdown.each(function () {
					$(this).change(function () {
						UpdateGrid(selector, controller, gridview, '', $(this).val(), searchword);
					});
				});
			});
		}
	});
})(jQuery);

function UpdateGridManafacturerView(griddiv, controller, gridview, col, page, searchword, manafid) {
    // when sorting, page is undefined or the same
    // when paging, column is the same
    var mvcgridvals = mvcgrids[griddiv];
    if (mvcgridvals == null) {
        mvcgridvals = {};
        mvcgridvals["currentpage"] = 1;
        mvcgridvals["direction"] = "ASC";
        mvcgridvals["sortcolumn"] = col;
    }
    else {
        if (page !== "" && page !== undefined && mvcgridvals["currentpage"] !== page) {
            // changing page
            mvcgridvals["currentpage"] = page;
        }
        else {
            // page being the same means we're sorting
            if (mvcgridvals["sortcolumn"] == col) {
                // same column means we need to switch ascending and descending
                if (mvcgridvals["direction"] == "ASC") {
                    mvcgridvals["direction"] = "DESC";
                }
                else {
                    mvcgridvals["direction"] = "ASC";
                }
            }
            else {
                // different column means we're starting with ascending
                mvcgridvals["direction"] = "ASC";
            }
            mvcgridvals["sortcolumn"] = col;
        }
    }
    // persist grid variables
    mvcgrids[griddiv] = mvcgridvals;
    // correcting for trailing slash -- no VirtualPathUtility here.
    var myhost = window.location.protocol + "//" + window.location.host
    var absoluteapp = myhost + applicationname;
    $("#loading").html("<img src=\"" + absoluteapp + imagedir + "/ajax-loader.gif\" />");
    $.ajax({
        type: "POST",
        url: absoluteapp + controller,
        data: ({ ColumnName: mvcgridvals["sortcolumn"], PageNum: mvcgridvals["currentpage"], Controller: controller, Griddiv: griddiv, GridView: gridview, Direction: mvcgridvals["direction"], SearchWord: searchword, id: manafid }),
        success: function(msg) {
            $(griddiv).html(msg);
            $(griddiv).mvcajaxManafacturerView(controller, gridview, searchword, manafid, { databinding: true });
            fixGrids(griddiv);
            $("#" + mvcgridvals["sortcolumn"] + "_Sort").html("&nbsp;&nbsp;<img src='" + absoluteapp + imagedir + "/" + mvcgridvals["direction"] + ".gif' />");
            $("#loading").html("&nbsp;");
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            alert("Data Failed to Sort: " + XMLHttpRequest.statusText);
            $("#loading").html("&nbsp;");
        }
    });
}

(function ($) {
	$.fn.extend({
		mvcajaxManafacturerView: function (controller, gridview, searchword, manafid, options) {
			var defaults = {
				defaultsort: '',
				databinding: false
			};

			var settings = $.extend(defaults, options);

			// which element are we applying this to?
			var selector = $(this).selector;

			if (!settings.databinding) {
				UpdateGridManafacturerView(selector, controller, gridview, settings.defaultsort, undefined, searchword, manafid);
				return false;
			}

			// helper function to parse querystring
			getPage = function (path) {
				var pagenum = 0;
				$.each(path.split("?")[1].split("&"),
                    function (key, val) {
                    	var params = val.split("=");
                    	pagenum = params[1];
                    	return (params[0] !== "page");
                    }
                );
				return pagenum;
			};

			return this.each(function () {
				// step 1 -- manipulate pager
				// step 1.a -- retrieve pager links
				var anchors = $(selector + " .pagination").children(".paginationRight").children("a");
				var pagenum;
				var updatelink = "javascript:UpdateGridManafacturerView('#0', '#1', '#2', '#3', '#4', '#5', '#6');";
				updatelink = updatelink
                        .replace("#0", selector)
                        .replace("#1", controller)
                        .replace("#2", gridview)
						.replace("#5", searchword)
						.replace("#6", manafid);
				anchors.each(function () {
					// step 1.b -- for each pager link, retrieve page linked to
					pagenum = getPage($(this).attr("href"));
					// step 1.c -- change page link to match controller 
					$(this).attr("href",
                      updatelink
                        .replace("#3", '')
                        .replace("#4", pagenum)
                    );
				});
				// step 2 -- manipulate header
				// step 2.a -- retrieve header items
				var header = $(selector + " .grid").children("thead").children("tr").children("th");
				var anchor;
				var sortcolumn;
				header.each(function () {
					// step 2.b -- manage links to javascript function
					anchor = $(this).children("a");
					// if there's no pre-existing link on the header 
					// (from a header override, maybe), we create a new one
					if (anchor.length == 0) {
						$(this).wrapInner("<a></a>");
					}
					anchor = $(this).children("a");
					sortcolumn = $(this).attr("sortcolumn");
					anchor.attr("href",
                      updatelink
                        .replace("#3", sortcolumn)
                        .replace("#4", '')
                    );
					// step 3 -- add span for ASC/DESC image
					$(this).append("<span id='" + sortcolumn + "_Sort'></span>");
				});
				// step 3 -- manipulate page dropdown
				// step 3.a -- retrieve page dropdown items
				var dropdown = $(selector + " .pagedropdown").children("select");
				dropdown.each(function () {
					$(this).change(function () {
						UpdateGridManafacturerView(selector, controller, gridview, '', $(this).val(), searchword, manafid);
					});
				});
			});
		}
	});
})(jQuery);