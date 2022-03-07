$(function(){

var mall;
var shop;
var shopItem;
var csvList;	 

	//-ショップ番号、モール種別、ショップIDデータの読み込み
	var shopList;
    $.ajax({
        url: 'csv/shop.csv?',
	    success: function(data) {
	    	shopList = $.csv()(data);    	
	    }
	});
	//-

//-セレクトボックス連動
    $("#MainContent_lstTarget_Shop").click(function(){		    

    //- 選択したショップ情報からモール情報とID情報の抽出
    for (var i = 1; i < shopList.length; i++) {
    	if(shopList[i][0] == $("#MainContent_lstTarget_Shop").val()){
		 mall = shopList[i][1];
		 shop = shopList[i][2];
		 shopItem = shopList[i][3];
    	}
    };
    //-

	if(mall == "ponpare"){
       $('#MainContent_ddlSearch31').attr('multiple','multiple');
       $('#MainContent_ddlSearch31').attr('size','3');
       $('#MainContent_ddlSearch32').attr('multiple','multiple');
       $('#MainContent_ddlSearch32').attr('size','3');
       $('#MainContent_ddlSearch33').attr('multiple','multiple');
       $('#MainContent_ddlSearch33').attr('size','3');
       $('#MainContent_ddlSearch34').attr('multiple','multiple');
       $('#MainContent_ddlSearch34').attr('size','3');
       $('#MainContent_ddlSearch75').attr('multiple','multiple');
       $('#MainContent_ddlSearch75').attr('size','3');
       $('#MainContent_ddlSearch76').attr('multiple','multiple');
       $('#MainContent_ddlSearch76').attr('size','3');
       $('#MainContent_ddlSearch77').attr('multiple','multiple');
       $('#MainContent_ddlSearch77').attr('size','3');
       $('#MainContent_ddlSearch78').attr('multiple','multiple');
       $('#MainContent_ddlSearch78').attr('size','3');
       $('#MainContent_ddlSearch79').attr('multiple','multiple');
       $('#MainContent_ddlSearch79').attr('size','3');
       $('#MainContent_ddlSearch710').attr('multiple','multiple');
       $('#MainContent_ddlSearch710').attr('size','3');

	}else{
       $('#MainContent_ddlSearch31').removeAttr('multiple');
       $('#MainContent_ddlSearch31').attr('size','1');
       $('#MainContent_ddlSearch32').removeAttr('multiple');
       $('#MainContent_ddlSearch32').attr('size','1');	
       $('#MainContent_ddlSearch33').removeAttr('multiple');
       $('#MainContent_ddlSearch33').attr('size','1');	
       $('#MainContent_ddlSearch34').removeAttr('multiple');
       $('#MainContent_ddlSearch34').attr('size','1');	
       $('#MainContent_ddlSearch75').removeAttr('multiple');
       $('#MainContent_ddlSearch75').attr('size','1');
       $('#MainContent_ddlSearch76').removeAttr('multiple');
       $('#MainContent_ddlSearch76').attr('size','1');
       $('#MainContent_ddlSearch77').removeAttr('multiple');
       $('#MainContent_ddlSearch77').attr('size','1');
       $('#MainContent_ddlSearch78').removeAttr('multiple');
       $('#MainContent_ddlSearch78').attr('size','1');
       $('#MainContent_ddlSearch79').removeAttr('multiple');
       $('#MainContent_ddlSearch79').attr('size','1');
       $('#MainContent_ddlSearch710').removeAttr('multiple');
       $('#MainContent_ddlSearch710').attr('size','1');
	}

	    $.ajax({
	        url: "csv/"+mall + '_condition.csv',
	        success: function(data) {
			$('#MainContent_ddlSearch11').children().remove();
			$('#MainContent_ddlSearch21').children().remove();
			$('#MainContent_ddlSearch31').children().remove();
			$('#MainContent_ddlSearch41').children().remove();
			$('#MainContent_ddlSearch12').children().remove();
			$('#MainContent_ddlSearch22').children().remove();
			$('#MainContent_ddlSearch32').children().remove();
			$('#MainContent_ddlSearch42').children().remove();
			$('#MainContent_ddlSearch13').children().remove();
			$('#MainContent_ddlSearch23').children().remove();
			$('#MainContent_ddlSearch33').children().remove();
			$('#MainContent_ddlSearch43').children().remove();
			$('#MainContent_ddlSearch14').children().remove();
			$('#MainContent_ddlSearch24').children().remove();
			$('#MainContent_ddlSearch34').children().remove();
			$('#MainContent_ddlSearch44').children().remove();

			$('#MainContent_ddlSearch55').children().remove();
			$('#MainContent_ddlSearch65').children().remove();
			$('#MainContent_ddlSearch75').children().remove();
			$('#MainContent_ddlSearch85').children().remove();


			$('#MainContent_ddlSearch56').children().remove();
			$('#MainContent_ddlSearch66').children().remove();
			$('#MainContent_ddlSearch76').children().remove();
			$('#MainContent_ddlSearch86').children().remove();


			$('#MainContent_ddlSearch57').children().remove();
			$('#MainContent_ddlSearch67').children().remove();
			$('#MainContent_ddlSearch77').children().remove();
			$('#MainContent_ddlSearch87').children().remove();


			$('#MainContent_ddlSearch58').children().remove();
			$('#MainContent_ddlSearch68').children().remove();
			$('#MainContent_ddlSearch78').children().remove();
			$('#MainContent_ddlSearch88').children().remove();

			$('#MainContent_ddlSearch59').children().remove();
			$('#MainContent_ddlSearch69').children().remove();
			$('#MainContent_ddlSearch79').children().remove();
			$('#MainContent_ddlSearch89').children().remove();

			$('#MainContent_ddlSearch510').children().remove();
			$('#MainContent_ddlSearch610').children().remove();
			$('#MainContent_ddlSearch710').children().remove();
			$('#MainContent_ddlSearch810').children().remove();

			
	            // csvを配列に格納
	            csvList = $.csv()(data);
	
	            // 検索条件1
	            var insert1 = '';
	            for (var i = 1; i < csvList.length; i++) {
	            	
	            	if(csvList[i][0] != ""){
//			$('#MainContent_ddlSearch11').append('<option value="'+csvList[i][0]+'">' + csvList[i][1] + '</option>');
//			$('#MainContent_ddlSearch12').append('<option value="'+csvList[i][0]+'">' + csvList[i][1] + '</option>');
//			$('#MainContent_ddlSearch13').append('<option value="'+csvList[i][0]+'">' + csvList[i][1] + '</option>');
//			$('#MainContent_ddlSearch14').append('<option value="'+csvList[i][0]+'">' + csvList[i][1] + '</option>');
			$('#MainContent_ddlSearch55').append('<option value="'+csvList[i][0]+'">' + csvList[i][1] + '</option>');
//			$('#MainContent_ddlSearch56').append('<option value="'+csvList[i][0]+'">' + csvList[i][1] + '</option>');
//			$('#MainContent_ddlSearch57').append('<option value="'+csvList[i][0]+'">' + csvList[i][1] + '</option>');
//			$('#MainContent_ddlSearch58').append('<option value="'+csvList[i][0]+'">' + csvList[i][1] + '</option>');
//			$('#MainContent_ddlSearch59').append('<option value="'+csvList[i][0]+'">' + csvList[i][1] + '</option>');
//			$('#MainContent_ddlSearch510').append('<option value="'+csvList[i][0]+'">' + csvList[i][1] + '</option>');
	            	}

	            	if(csvList[i][2] != ""){
			$('#MainContent_ddlSearch21').append('<option value="'+csvList[i][2]+'">' + csvList[i][3] + '</option>');
			$('#MainContent_ddlSearch22').append('<option value="'+csvList[i][2]+'">' + csvList[i][3] + '</option>');
			$('#MainContent_ddlSearch23').append('<option value="'+csvList[i][2]+'">' + csvList[i][3] + '</option>');
			$('#MainContent_ddlSearch24').append('<option value="'+csvList[i][2]+'">' + csvList[i][3] + '</option>');
			$('#MainContent_ddlSearch65').append('<option value="'+csvList[i][2]+'">' + csvList[i][3] + '</option>');
			$('#MainContent_ddlSearch66').append('<option value="'+csvList[i][2]+'">' + csvList[i][3] + '</option>');
			$('#MainContent_ddlSearch67').append('<option value="'+csvList[i][2]+'">' + csvList[i][3] + '</option>');
			$('#MainContent_ddlSearch68').append('<option value="'+csvList[i][2]+'">' + csvList[i][3] + '</option>');
			$('#MainContent_ddlSearch69').append('<option value="'+csvList[i][2]+'">' + csvList[i][3] + '</option>');
			$('#MainContent_ddlSearch610').append('<option value="'+csvList[i][2]+'">' + csvList[i][3] + '</option>');
	            	}

	            	if(csvList[i][4] != ""){
			$('#MainContent_ddlSearch31').append('<option value="'+csvList[i][4]+'">' + csvList[i][5] + '</option>');
			$('#MainContent_ddlSearch32').append('<option value="'+csvList[i][4]+'">' + csvList[i][5] + '</option>');
			$('#MainContent_ddlSearch33').append('<option value="'+csvList[i][4]+'">' + csvList[i][5] + '</option>');
			$('#MainContent_ddlSearch34').append('<option value="'+csvList[i][4]+'">' + csvList[i][5] + '</option>');
			$('#MainContent_ddlSearch75').append('<option value="'+csvList[i][4]+'">' + csvList[i][5] + '</option>');
			$('#MainContent_ddlSearch76').append('<option value="'+csvList[i][4]+'">' + csvList[i][5] + '</option>');
			$('#MainContent_ddlSearch77').append('<option value="'+csvList[i][4]+'">' + csvList[i][5] + '</option>');
			$('#MainContent_ddlSearch78').append('<option value="'+csvList[i][4]+'">' + csvList[i][5] + '</option>');
			$('#MainContent_ddlSearch79').append('<option value="'+csvList[i][4]+'">' + csvList[i][5] + '</option>');
			$('#MainContent_ddlSearch710').append('<option value="'+csvList[i][4]+'">' + csvList[i][5] + '</option>');
	            	}

	            	if(csvList[i][6] != ""){
			$('#MainContent_ddlSearch41').append('<option value="'+csvList[i][6]+'">' + csvList[i][7] + '</option>');
			$('#MainContent_ddlSearch42').append('<option value="'+csvList[i][6]+'">' + csvList[i][7] + '</option>');
			$('#MainContent_ddlSearch43').append('<option value="'+csvList[i][6]+'">' + csvList[i][7] + '</option>');
			$('#MainContent_ddlSearch44').append('<option value="'+csvList[i][6]+'">' + csvList[i][7] + '</option>');
			$('#MainContent_ddlSearch85').append('<option value="'+csvList[i][6]+'">' + csvList[i][7] + '</option>');
			$('#MainContent_ddlSearch86').append('<option value="'+csvList[i][6]+'">' + csvList[i][7] + '</option>');
			$('#MainContent_ddlSearch87').append('<option value="'+csvList[i][6]+'">' + csvList[i][7] + '</option>');
			$('#MainContent_ddlSearch88').append('<option value="'+csvList[i][6]+'">' + csvList[i][7] + '</option>');
			$('#MainContent_ddlSearch89').append('<option value="'+csvList[i][6]+'">' + csvList[i][7] + '</option>');
			$('#MainContent_ddlSearch810').append('<option value="'+csvList[i][6]+'">' + csvList[i][7] + '</option>');

	            	}
	            };
$('#MainContent_ddlSearch55').val("aaaa")
	            //-
	        }
	    });
	});
	//-

	//-URL作成の実行
	$(".productPageGenerate").click(function(){
	var result;
	condition1 = $("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch1$1')).val();
	condition2 = $("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch2$1')).val();
	condition3 = $("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch3$1')).val();
	condition4 = $("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch4$1')).val();
	condition5 = $("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch5$1')).val();
	condition6 = $("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch6$1')).val();
	condition7 = $("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch7$1')).val();
	condition8 = $("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch8$1')).val();
	condition9 = $("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch9$1')).val();
	condition10 = $("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch10$1')).val();
	item = $("#MainContent_txtURL"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, '$1')).val();
//alert($("#MainContent_txtURL"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, '$1')).val().split(",").length);

		//-楽天モールの場合
		if(mall == 'rakuten'){
//		item = ($("#MainContent_txtURL"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, '$1')).val().replace(/,/g , "+"));
			
			//-カンマが1以上の時は検索ページのURLを生成
			if(item.split(",").length > 1){
			item = item.replace(/,/g , "+");
			result = "http://search.rakuten.co.jp/search/inshop-mall?f=1&"+condition4+"&sid="+shop+"&uwd=1&"+condition3+"&p=1&sitem="+item+"&"+condition2+"&"+condition1+"&nitem=&min=&max=";
			
			//-カンマが存在しない場合商品ページのURLを生成
			}else{
			result = "http://item.rakuten.co.jp/"+shopItem+"/"+item+"/";
			}
			//-


		//-ヤフーモールの場合
		}else if(mall == 'yahoo'){
//		item = ($("#MainContent_txtURL"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, '$1')).val().replace(/,/g , " "));

			//-カンマが1以上の時は検索ページのURLを生成
			if(item.split(",").length > 1){
			item = item.replace(/,/g , " ");
			result = "http://store.shopping.yahoo.co.jp/"+shop+"/search.html?"+condition1+item+"&"+condition2+"&"+condition3;
			
			//-カンマが存在しない場合商品ページのURLを生成
			}else{
			result = "http://store.shopping.yahoo.co.jp/"+shopItem+"/"+item+".html";
			}


		//-ポンパレモールの場合
		}else if(mall == 'ponpare'){
//		item = ($("#MainContent_txtURL"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, '$1')).val().replace(/,/g , "+"));

			//-カンマが1以上の時は検索ページのURLを生成
			if(item.split(",").length > 1){
			item = item.replace(/,/g , "+");
			var checkOption = "";

			        $($("#MainContent_"+$(this).attr("id").toString().replace(/productPageGenerate([0-9])/g, 'ddlSearch3$1')+" option:selected")).each(function(){
			            checkOption = checkOption + "&" + $(this).val();
			        });

			result = "http://store.ponparemall.com/"+shop+"/search/?keywordAnd="+item+checkOption+"&minAmount=&maxAmount=&"+condition1;

			//-カンマが存在しない場合商品ページのURLを生成
			}else{
			result = "http://store.ponparemall.com/"+shopItem+"/goods/"+item+"/";
			}			
		}
		//-

	$("#"+$(this).attr("id").toString().replace("productPageGenerate" , "MainContent_txtProductPage")).val(result);
	});	
	
});
//-
