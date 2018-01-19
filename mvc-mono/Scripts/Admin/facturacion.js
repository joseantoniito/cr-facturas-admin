// Write your Javascript code.

$(document).ready(function(){
	
	function load_facturas(){

		function create_grid_factura_manual(item_str){  
			var form = $("#window_{0} form".format(item_str));
			if($("#grid_{0}".format(item_str)).length == 0) return;
			var grid;
			// FacturaManualId, EmpresaId, Registrado, UsuarioId, Consecutivo, Observaciones, 
			//EstadoFactura, Moneda, MontoTotal, MontoCobrado, MontoDescuento, MontoBruto, TipoPago, 
			//ArchivoXMLId, ClienteId
			var columns = [
				//{field: "Registrado", title: "Registrado"},
				{field: "Consecutivo", title: "Consecutivo"},
				//{field: "EstadoFactura", title: "Estado Factura"},
				{field: "Moneda", title: "Moneda", template: "<span id='Moneda'>#:Moneda#</span>"},
				//{field: "MontoBruto", title: "Monto Bruto"},
				{field: "MontoDescuento", title: "Monto Descuento"},
				//{field: "MontoTotal", title: "Monto Total"},
				{field: "MontoCobrado", title: "Monto Cobrado"},
				{field: "TipoPago", title: "Tipo de Pago", template: "<span id='TipoPago'>#:TipoPago#</span>"},
				{field:"FacturaManualId", title:"Acciones", width:"100px", 
				template: "<a  id='btn_ver' href='/Facturacion/Item/#:FacturaManualId#'><i _id='#:ClienteId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i></a>"}
			];

			function data_bound_factura_manual(e){
				var get_tipo_pago = function(id){
					return $.grep(tiposPago, function(item){
						return item.Id == id;
					})[0].Nombre;
				}
				var get_tipo_moneda = function(id){
					return $.grep(tiposMoneda, function(item){
						return item.Id == id;
					})[0].Nombre;
				}
				$.each(e.sender.items(), function(index, item){
					item = $(item);
					//item.find("#btn_editar").on("click", edit_item);
					item.find("#TipoPago").text( get_tipo_pago(item.find("#TipoPago").text()) );
					item.find("#Moneda").text( get_tipo_moneda(item.find("#Moneda").text()) );
				});
			}; 

			//INIT
			jQuery("#btn_add_{0}".format(item_str)).attr("href", "/Facturacion/Item/");
			$.get('/Facturacion/ObtenerFacturaManuals/', function(response) {
				grid = create_grid(item_str, response, columns, data_bound_factura_manual);
			}, 'json');
		}

		function create_edit_window_factura_manual(item_str){
			//todo: if(!$("#window_"+item_str).is(":visible")) return;
			var form = $("#window_{0} form".format(item_str));
			var window, validator;

			function edit_item(event, data){
				console.log("entro a editar");
				var sender = $(event.currentTarget);
				var id = sender.attr("_id");
				form.find("[name='FacturaManualId']").val(id);
				
				var item = sender.parent().parent();
				//var data = $("#grid_"+item_str).data("kendoGrid").dataItem(item);
				console.log("data edit" , data);

				response = data;
				set_data_object_to_form("#window_{0} form".format(item_str), data);

				//$("#window_"+item_str).data("kendoWindow").center().open();
			}

			function clear_controls(){
				form.find("input[name]").val("");
				form.find("[name='FacturaManualId']").val("0");
			}

			function register_item(){
				if(!validator.validate()) return false;

				var url = '/Facturacion/GuardarFacturaManual';
				var data = get_data_object_from_form("#window_{0} form".format(item_str));
				data.MontoTotal = form.find("[name='MontoTotal']").val();
				var fn_compare_id = function(item, response){
						return item.ClienteId == response.ClienteId;
					}
				post_url(url, data, item_str, fn_compare_id);
				return false;
			}

			function load_catalogs(){
				form.find("[name='TipoPago']").kendoDropDownList({
	                dataTextField: "Nombre",
	                dataValueField: "Id",
	                dataSource: tiposPago
	            }).data("kendoDropDownList");

	            form.find("[name='Moneda']").kendoDropDownList({
	                dataTextField: "Nombre",
	                dataValueField: "Id",
	                dataSource: tiposMoneda
	            }).data("kendoDropDownList");
			}

			function load_form_scripts(){
				var on_key_up_monto = function(){
					var monto_total = $("[name='MontoBruto']").val() - $("[name='MontoDescuento']").val();
					$("[name='MontoTotal']").val(monto_total);
					$("[name='MontoCobrado']").val(monto_total);
				}

				$("[name='MontoBruto']").on("keyup", on_key_up_monto);
				$("[name='MontoDescuento']").on("keyup", on_key_up_monto);
			}


			if(get_id_from_url() != 0)
				$.get('/Facturacion/ObtenerFacturaManual/'+get_id_from_url(), function(response) {
					edit_item({ currentTarget: $("<span></span>").attr("_id", get_id_from_url()) }, response);
				}, 'json');
			//window = create_window_controls(item_str, clear_controls);
			validator = load_form_controls(item_str, register_item);
			load_catalogs();
			load_form_scripts();
		}



		//FACTURA MANUAL LÍNEA
		function create_grid_factura_manual_linea(item_str){  
			var form = $("#window_{0} form".format(item_str));
			if($("#grid_{0}".format(item_str)).length == 0) return;
			var grid;
			//FacturaManualId, FacturaManualLineaId, TipoItem, ObservacionAdicional, MontoLineaCobrado, 
			//PrecioUnitario, Cantidad, MontoLineaCalculado, Codigo, ItemId
			var columns = [
				{field: "TipoItem", title: "Tipo de Item", template: "<span id='TipoItem'>#:TipoItem#</span>"},
				//{field: "ObservacionAdicional", title: "Observación Adicional"},
				//{field: "PrecioUnitario", title: "Precio Unitario"},
				//{field: "Cantidad", title: "Cantidad"},
				{field: "MontoLineaCobrado", title: "Monto Línea Cobrado"},
				//{field: "MontoLineaCalculado", title: "Monto Línea Calculado"},
				{field: "Codigo", title: "Código"},
				{field: "ItemId", title: "Item", template: "<span id='ItemId'>#:ItemId#</span>"},
				{field:"FacturaManualLineaId", title:"Acciones", width:"100px", 
				template: "<i _id='#:FacturaManualLineaId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i>"}
			];

			function edit_item(event){
				console.log("entro a editar");
				var sender = $(event.currentTarget);
				var id = sender.attr("_id");
				form.find("[name='FacturaManualLineaId']").val(id);
				
				var item = sender.parent().parent();
				var data = $("#grid_"+item_str).data("kendoGrid").dataItem(item);
				console.log("data edit" , data);

				response = data;
				set_data_object_to_form("#window_{0} form".format(item_str), data);

				form.find("[name='FacturaManualId']").val(get_id_from_url());

				$("#window_"+item_str).data("kendoWindow").center().open();
			}

			function data_bound_factura_manual_linea(e){
				var get_tipo = function(id){
					return $.grep(tiposItem, function(item){
						return item.TipoItem == id;
					})[0].Nombre;
				}
				var get_item = function(id){
					var items_data = form.find("[name='ItemId']").data("kendoDropDownList").data();
					return $.grep(items_data, function(item){
						return item.ItemId == id;
					});

				}

				$.each(e.sender.items(), function(index, item){
					item = $(item);
					item.find("#btn_editar").on("click", edit_item);
					item.find("#TipoItem").text( get_tipo(item.find("#TipoItem").text()) );
					item.find("#Item").text( get_item(item.find("#Item").text()) );
				});
			}; 

			//INIT
			$.get('/Facturacion/ObtenerFacturaManualLineas/'+get_id_from_url(), function(response) {
				grid = create_grid(item_str, response, columns, data_bound_factura_manual_linea);
			}, 'json');
		}

		function create_edit_window_factura_manual_linea(item_str){
			//todo: if(!$("#window_"+item_str).is(":visible")) return;
			var form = $("#window_{0} form".format(item_str));
			var window, validator;



			function clear_controls(){
				form.find("input[name]").val("");
				form.find("[name='FacturaManualLineaId']").val("0");
			}

			function register_item(){
				if(!validator.validate()) return false;

				var url = '/Facturacion/GuardarFacturaManualLinea';
				var data = get_data_object_from_form("#window_{0} form".format(item_str));
				data.MontoLineaCalculado = form.find("[name='MontoLineaCalculado']").val();
				var fn_compare_id = function(item, response){
						return item.FacturaManualLineaId == response.FacturaManualLineaId;
					}
				post_url(url, data, item_str, fn_compare_id);
				return false;
			}

			function load_catalogs(){
				form.find("[name='TipoItem']").kendoDropDownList({
	                dataTextField: "Nombre",
	                dataValueField: "TipoItem",
	                dataSource: tiposItem
	            }).data("kendoDropDownList");

	            $.get('/Items/ObtenerItems/', function(response) {
					form.find("[name='ItemId']").kendoDropDownList({
		                dataTextField: "Descripcion",
		                dataValueField: "ItemId",
		                dataSource: response
		            }).data("kendoDropDownList");
				}, 'json');
	            
			}

			//FacturaManualId, FacturaManualLineaId, TipoItem, ObservacionAdicional, MontoLineaCobrado, 
			//PrecioUnitario, Cantidad, MontoLineaCalculado, Codigo, ItemId
			function load_form_scripts(){
				var on_key_up_monto = function(){
					var monto_total = $("[name='PrecioUnitario']").val() * $("[name='Cantidad']").val();
					$("[name='MontoLineaCalculado']").val(monto_total);
					$("[name='MontoLineaCobrado']").val(monto_total);
				}

				$("[name='PrecioUnitario']").on("keyup", on_key_up_monto);
				$("[name='Cantidad']").on("keyup", on_key_up_monto);
			}

			/*if(get_id_from_url() != 0)
				$.get('/Facturacion/ObtenerFacturaManualLinea/'+get_id_from_url(), function(response) {
					edit_item({ currentTarget: $("<span></span>").attr("_id", get_id_from_url()) }, response);
				}, 'json');*/
			load_catalogs();
			window = create_window_controls(item_str, clear_controls);
			validator = load_form_controls(item_str, register_item);
			load_form_scripts();
		}

		//INIT
		create_grid_factura_manual("factura_manuals");
		create_edit_window_factura_manual("factura_manuals");

		create_grid_factura_manual_linea("factura_manual_lineas");
		create_edit_window_factura_manual_linea("factura_manual_lineas");
	}

	//INIT MASTER
	load_facturas();			
});




