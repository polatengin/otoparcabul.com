String.prototype.trim = function () {
  return this.replace(/^\s+|\s+$/g, "");
}
String.prototype.ltrim = function () {
  return this.replace(/^\s+/, "");
}
String.prototype.rtrim = function () {
  return this.replace(/\s+$/, "");
}

if (!Array.prototype.indexOf) {
  Array.prototype.indexOf = function (elt /*, from*/) {
    var len = this.length >>> 0;

    var from = Number(arguments[1]) || 0;
    from = (from < 0)
      ? Math.ceil(from)
      : Math.floor(from);
    if (from < 0)
      from += len;

    for (; from < len; from++) {
      if (from in this &&
        this[from] === elt)
        return from;
    }
    return -1;
  };
}

(function ($) {
  $.fn.equalHeights = function (minHeight, maxHeight) {
    tallest = (minHeight) ? minHeight : 0;
    this.each(function () {
      if ($(this).height() > tallest) {
        tallest = $(this).height();
      }
    });
    if ((maxHeight) && tallest > maxHeight) tallest = maxHeight;
    return this.each(function () {
      $(this).height(tallest).css("overflow", "hidden");
    });
  }
})(jQuery);
