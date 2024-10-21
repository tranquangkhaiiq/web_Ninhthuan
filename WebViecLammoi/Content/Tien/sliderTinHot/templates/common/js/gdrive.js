/* gdrive create iframe */
!function() {
    var scripts = document.getElementsByTagName("script"),
        src = scripts[scripts.length-1].src;
    if(src && /gdrive.js/g.test(src)) {
        src = src.replace('gdrive.js', '');
        var result = '';

        // styles
        result += [
            '<link rel="stylesheet" type="text/css" href="$CssPath$style.css" />',
            typeof jQuery == 'undefined'
                ?('<script type="text/javascript" src="$JsPath$jquery.js"></script>')
                :'',
            '$headScripts$'
        ].join('');


        // slider
        result += [
            '<div id="wowslider-container$GallerySuffix$">',
            '    $ImageListGdrive$',
            '    <div class="ws_shadow"></div>',
            '</div>'
        ].join('');


        // scripts
        result += [
            '<script type="text/javascript" src="$JsPath$wowslider.js"></script>',
            '<script type="text/javascript" src="$JsPath$script.js"></script>'
        ].join('');


        // replace all links
        result = result.replace(RegExp('$exportPath$', 'g'), src + '$exportPath$')
                       //.replace(RegExp('$JsPath$', 'g'), src + '$JsPath$')
                       .replace(RegExp('$CssPath$', 'g'), src + '$CssPath$');


        // write result
        document.write(result);
    }
}()