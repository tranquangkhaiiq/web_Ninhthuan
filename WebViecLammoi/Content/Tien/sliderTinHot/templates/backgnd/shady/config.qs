/* config.js */
params.ContaienerW = imageW + backMargins.left + backMargins.right;
params.ContaienerH = imageH + backMargins.bottom;

thumbs = {margin: 3, padding: 5};
if(!parseInt(params.noFrame)){
	params.ShadowW = Math.round(params.ContaienerW*1.4);
	params.ShadowH = Math.round(params.ContaienerH/2.12);
	files.push( { 'src': 'backgnd/'+params.TemplateName+'/style-shadow.css', 'dest': '$CssPath$style.css', 'filters': ['params'] } );
	files.push({ 'src': 'backgnd/'+params.TemplateName+'/shadow.png', 'filters': [ { 'name': 'resize', 'width': params.ShadowW, 'height': params.ShadowH } ] });
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

// call this function at the end of each template
finalize();