// Write your Javascript code.

$(document).ready(function(){
	
	function load_items_certificados(){
		var item_str = "certificados";
		var form = $("#window_{0} form".format(item_str));

		function create_grid_items(){  
			if($("#grid_{0}".format(item_str)).length == 0) return;
			var grid;
			//CertificadoId, EmpresaId, RutaCertificado, Registrado, Status, ClaveArchivo
			var columns = [
				{field: "RutaCertificado", title: "RutaCertificado"},
				{field: "Registrado", title: "Registrado"},
				{field: "Status", title: "Status"},
				{field: "ClaveArchivo", title: "ClaveArchivo"},
				{field:"CertificadoId", title:"Acciones", width:"100px", 
				template: "<a  id='btn_ver' href='/Certificados/Item/#:CertificadoId#'><i _id='#:CertificadoId#' id='btn_editar' class='fa fa-pencil-square' title='Editar'></i></a>"}
			];

			function data_bound_items(e){
				$.each(e.sender.items(), function(index, item){
					item = $(item);
					//item.find("#btn_editar").on("click", edit_item);
				});
			}; 

			//INIT
			$.get('/Certificados/ObtenerCertificados/', function(response) {
				grid = create_grid(item_str, response, columns, data_bound_items);
			}, 'json');
		}

		function create_edit_window(){
			if(!$("#window_"+item_str).is(":visible")) return;
			var window, validator;

			function edit_item(event, data){
				console.log("entro a editar");
				var sender = $(event.currentTarget);
				var id = sender.attr("_id");
				form.find("[name='CertificadoId']").val(id);
				
				var item = sender.parent().parent();
				//var data = $("#grid_"+item_str).data("kendoGrid").dataItem(item);
				console.log("data edit" , data);

				response = data;
				set_data_object_to_form("form", data);
				//$("#window_"+item_str).data("kendoWindow").center().open();
			}

			function clear_controls(){
				form.find("input[name]").val("");
				form.find("[name='CertificadoId']").val("0");
				//$( "form input[name]" ).val("");
			}

			function register_item(){
				if(!validator.validate()) return false;

				var url = '/Certificados/GuardarCertificado';
				var data = get_data_object_from_form("form");
				var fn_compare_id = function(item, response){
						return item.servicioId == response.servicioId;
					}
				post_url(url, data, item_str, fn_compare_id);
				return false;
			}

			function create_form_controls(){
				var files_upload = [];
				$("#upload_certificado").kendoUpload({
		            async: {
		                saveUrl: "/Admin/Save",
		                removeUrl: "/Admin/Remove",
		                autoUpload: true
		            },
		            files: files_upload,
		            validation: {
		                allowedExtensions: [".jpg", ".png", "pdf", "xls", "xlsx", "doc", "docx"],
		            },
		            success: function(e){
		                
		                if(e.operation == "upload"){
		                	console.log("save", e);
		                	$("[name='RutaCertificado']").val(e.files[0].name);
		                    /*var id_media_item = e.response.id;
		                    if(id_media_item){
		                        var nombre_logotipo = e.files[0].name;
		                        $("[name='logotipo']").val(nombre_logotipo);
		                        add_development_logo(id_media_item, nombre_logotipo);
		                    }*/
		                }
		                else if(e.operation == "remove"){
		                	console.log("error", e);
		                	$("[name='RutaCertificado']").val("");
		                    /*var id_media_item = e.files[0].id;
		                    $("[name='logotipo']").val("");
		                    delete_development_logo(id_media_item);*/
		                }
		            },
		            error: function(e){
		                console.log("error", e);
		            },
		            select: function(e){
		                console.log("select", e);
		                /*if(this.getFiles().length > 0){
		                    //alert("Solo puedes cargar una imágen.");
		                    //e.preventDefault();
		                    this.removeAllFiles();
		                }*/
		            }
		        }).data("kendoUpload");
			}

			if(get_id_from_url() != 0)
				$.get('/Certificados/ObtenerCertificado/'+get_id_from_url(), function(response) {
					edit_item({ currentTarget: $("<span></span>").attr("_id", get_id_from_url()) }, response);
				}, 'json');
			//window = create_window_controls(item_str, clear_controls);
			validator = load_form_controls(item_str, register_item);
			create_form_controls();
		}

		//INIT
		create_grid_items();
		create_edit_window();
	}

	//INIT MASTER
	load_items_certificados();			
});




