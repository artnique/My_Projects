var tim = 1;
var i = setInterval("tim++", 10);
 
function auto(url, b) {
    $("#sudu" + b).val(tim + "ms");
}
function run() {
    for (var i = 1; i < autourl.length; i++) {
        document.write("<img name=\"suduname\" src=\"" + autourl[i] + "/" + "\" width=\"1\" height=\"1\" onerror=\"auto('" + autourl[i] + "'," + i + ")\" style=\"display:none\" />");
    }
}
run();