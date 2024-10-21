/* config.js */
params.ContaienerW = imageW + backMargins.left + backMargins.right;
params.PageBgColor = params.PageBgColor||"#d7d7d7";
slideshow_css = '$CssPath$style.css';

thumbs = {margin: 3, padding: 2};
if(!parseInt(params.noFrame)){
	files.push( { 'src': 'backgnd/'+params.TemplateName+'/style-shadow.css', 'dest': '$CssPath$style.css', 'filters': ['params'] } );
	files.push({ 'src': 'backgnd/'+params.TemplateName+'/shadow.png', 'filters': [ { 'name': 'resize', 'width': params.ImageWidth } ] });
}

files.push({ 'src': 'backgnd/'+params.TemplateName+'/bullet.png' });
files.push({ 'src': 'backgnd/'+params.TemplateName+'/arrows.png' });
files.push({ 'src': 'backgnd/'+params.TemplateName+'/play.png' });
files.push({ 'src': 'backgnd/'+params.TemplateName+'/pause.png' });


if (params.Thumbnails || params.ShowBullets){
	params.ThumbWidthHalf = Math.round(params.ThumbWidth/2);
	files.push(	{ 'src': 'backgnd/'+params.TemplateName+'/triangle-'+params.TooltipPos+'.png', dest: '$ImgPath$triangle.png' } );
	files.push( { 'src': 'backgnd/'+params.TemplateName+'/style-tooltip.css', 'dest': '$CssPath$style.css', 'filters': ['params'] } );
}

params.addWidth = 42; // arrow width

// call this function at the end of each template
finalize();