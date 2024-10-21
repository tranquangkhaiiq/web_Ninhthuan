/* config.js */
params.prevCaption = 'prev';
params.nextCaption = 'next';

slideshow_css = '$CssPath$style.css';

thumbs = {margin: 3, padding: 2};
files.push({ 'src': 'backgnd/'+params.TemplateName+'/play.png' });
files.push({ 'src': 'backgnd/'+params.TemplateName+'/pause.png' });


if (params.Thumbnails || params.ShowBullets){
	params.ThumbWidthHalf = Math.round(params.ThumbWidth/2);
	files.push( { 'src': 'backgnd/'+params.TemplateName+'/style-tooltip.css', 'dest': slideshow_css, 'filters': ['params'] } );
}

// call this function at the end of each template
finalize();