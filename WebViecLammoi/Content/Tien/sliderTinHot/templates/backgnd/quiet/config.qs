/* config.js */

slideshow_css = '$CssPath$style.css';

thumbs = {margin: 3, padding: 5};
if (!parseInt(params.noFrame)){
	// frame border+shadow
	params.frameL = 0;
	params.frameT = 100;
	params.frameW = 100;
	params.frameH = 20;

	// when thumbnails - fix shadow
	if (params.Thumbnails && (params.ThumbAlign=="top" || params.ThumbAlign=="bottom")){
		var thumbFullHeight = (thumbs.margin + thumbs.padding) * 2 + parseInt(params.ThumbHeight) + 15; // 15 is magic number :)
		var thumbFullHeightPercent = 100 * thumbFullHeight / imageH;
		params.frameT -= thumbFullHeightPercent;
	}

	files.push({ 'src': 'backgnd/'+params.TemplateName+'/shadow.png',     'filters': [ { 'name': 'resize', 'width': imageW, 'height': imageH*0.2, 'margins': border } ] });
	files.push( { 'src': 'backgnd/'+params.TemplateName+'/style-shadow.css', 'dest': slideshow_css, 'filters': ['params'] } );
	
	params.BulletsBottom = -24;
	params.backMarginsTop    += border.top;
}
else{
	params.BulletsBottom = 5;
}

params.decorW = params.ImageWidth - 8*2;
params.decorH = params.ImageHeight - 8*2;

files.push({ 'src': 'backgnd/'+params.TemplateName+'/bullet.png' });
files.push({ 'src': 'backgnd/'+params.TemplateName+'/arrows.png' });
files.push({ 'src': 'backgnd/'+params.TemplateName+'/play.png' });
files.push({ 'src': 'backgnd/'+params.TemplateName+'/pause.png' });


if (params.Thumbnails || params.ShowBullets){
	params.ThumbWidthHalf = Math.round(params.ThumbWidth/2);
	files.push(	{ 'src': 'backgnd/'+params.TemplateName+'/triangle-'+params.TooltipPos+'.png', dest: '$ImgPath$triangle.png' } );
	files.push( { 'src': 'backgnd/'+params.TemplateName+'/style-tooltip.css', 'dest': slideshow_css, 'filters': ['params'] } );
}


// call this function at the end of each template
finalize();