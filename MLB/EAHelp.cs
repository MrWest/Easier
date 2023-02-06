using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLB
{
    enum HelpKind { Descripcion, Resolucion, Consejos };
    enum ErrorKind { SM_Desc_Inicio1, SM_Desc_Inicio2, SM_Desc_Inicio3, SM_Desc_IPV, SM_Desc_RIPV, RM_Wront_Prod, IPV_Wront_Prod, SM_Desc_ChkIPV,FC_CHK_IPV,SM_Desc_Prec,SM_Ramal_SubZero, SM_Desc_UnKnown,  };// inicio1 no el submayor anterior no ha sido guardado, 2 inicio incoherente en ramal 3 en ipv

    class EAHelp
    {
        public HelpKind helpKind;

        public EAHelp()
        {
            helpKind = HelpKind.Descripcion;
        }
        public String HelpText(HlpMgr hmgr)
        {
            if (hmgr.hlpknd == HelpKind.Descripcion)
            {
           
                if (hmgr.topic == "Saludo")
                {
                    if (hmgr.tnumber == 1)
                    {
                        return "Hola... Soy Eve y voy ayudarte a utilizar el Easier.";

                    }
                    if (hmgr.tnumber == 2)
                    {
                        return "Voy a estar orientándote y asesorándote a lo largo del proceso de Gestión de tu Contabilidad...";

                    }

                    if (hmgr.tnumber == 3)
                    {
                        return "Si desea profundizar en los temas de ayuda...Puedes dar clic encima de mi para consultar la Ayuda General.";

                    }

                }
                if (hmgr.topic == "SubMayor")
                {
                    if (hmgr.tnumber == 1)
                    {
                        return "El SubMayor refleja el importe de las existencias y movimientos de productos para esta cuenta.";

                    }
                    if (hmgr.tnumber == 2)
                    {
                        return "La última columna (Comprobacion Contra Ramal), indica si hay o no descuadre en su contabilidad.";

                    }
                    if (hmgr.tnumber == 3)
                    {
                        return "Pasando el puntero del mouse por las celdas de Entrada Int. y Salida Int. se pueden ver los detalles de las mismas.";

                    }
                }
                if (hmgr.topic == "Ramal20")
                {
                    if (hmgr.tnumber == 1)
                    {
                        return "El Ramal20 Ramal es el documento principal del sistema, representa la existencia de productos en el almacén.";

                    }
                    if (hmgr.tnumber == 2)
                    {
                        return "Refleja además la entrada, salida (hacia los puestos de venta) y los traslados de los mismos, entre otros aspectos.";

                    }
                    if (hmgr.tnumber == 3)
                    {
                        return "Cada fila representa los datos de un producto almacenado específico.";

                    }
                }

                if (hmgr.topic == "IPV")
                {
                    if (hmgr.tnumber == 1)
                    {
                        return "IPV: representa la existencia de productos en los puestos de ventas. ";

                    }
                    if (hmgr.tnumber == 2)
                    {
                        return "Refleja además la elaboración de productos, entre otros aspectos.";

                    }
                    if (hmgr.tnumber == 3)
                    {
                        return "Al igual que el Ramal20, establece una relación Cantidad-Precio-Importe donde Cantidad*Precio= Importe.";

                    }
                }
                if (hmgr.topic == "Ficha de Costo")
                {
                    if (hmgr.tnumber == 1)
                    {
                        return "Ficha de Costo: representa la información resultante de la elaboración de un producto a partir de otros.";

                    }
                    if (hmgr.tnumber == 2)
                    {
                        return "Este documento ordenada dicha información de forma legible y comprensible...";

                    }
                    if (hmgr.tnumber == 3)
                    {
                        return "Además permite hacer pequeños ajustes en su única columna editable: Importe.";

                    }
                }
                if (hmgr.topic == "Vale de Salida")
                {
                    if (hmgr.tnumber == 1)
                    {
                        return "Vale de Salida: representa la información de cantidades e importes totalizados...";

                    }
                    if (hmgr.tnumber == 2)
                    {
                        return "de los productos que han salido del almacen hacia los puntos de venta o internamente hacia otras cuentas";

                    }
                    if (hmgr.tnumber == 3)
                    {
                        return "Es totalmente informativo... No se puede editar.";

                    }
                }
                if (hmgr.topic == "Flujo de Caja")
                {
                    if (hmgr.tnumber == 1)
                    {
                        return "Flujo de Caja: representa las entradas y salidas de efectivo de una determinada entidad...";

                    }
                    if (hmgr.tnumber == 2)
                    {
                        return "Cuenta con un filtro que permite obtener dicha información para un determinado período de tiempo...";

                    }
                    if (hmgr.tnumber == 3)
                    {
                        return "Los balances aparecen en las esquinas superiores...";

                    }
                }

                if (hmgr.topic == "Resumen de Ingresos")
                {
                    if (hmgr.tnumber == 1)
                    {
                        return "Resumen de Ingresos: representa el valor monetario de las operaciones...";

                    }
                    if (hmgr.tnumber == 2)
                    {
                        return " o sea el costo y los ingresos producto de las ventas y/o prestaciones de servicios. ";

                    }
                    if (hmgr.tnumber == 3)
                    {
                        return "Las columnas de los acumulados son editables para casos se inicios contables..";

                    }
                }

            }

            if (hmgr.hlpknd == HelpKind.Resolucion)
            {

                if (hmgr.topic == "SubMayor")
                {
                    if (hmgr.errknd == ErrorKind.SM_Desc_Inicio1)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            if (hmgr.atext.Count > 0)
                                return "Tienes un Descuadre en el SubMayor.\nEl descuadre es de: $ " + hmgr.atext[0].ToString() + ".";
                            else
                                return "Tienes un Descuadre en el SubMayor.";

                        }
                        if (hmgr.tnumber == 2)
                        {
                            return "Falta el Inicio del Dia... o el valor es incorrecto...";


                        }
                        if (hmgr.tnumber == 3)
                        return " Debe Guardar el SubMayor del dia anterior si existe de lo contrario escriba el inicio usted mismo en la celda Saldo Inicial.";
                    }

                    if (hmgr.errknd == ErrorKind.SM_Desc_RIPV)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            if (hmgr.atext.Count > 0)
                                return "Tienes un Descuadre en el SubMayor de $ " + hmgr.atext[0].ToString();
                            else
                                return "Tienes un Descuadre en el SubMayor"; 

                        }
                        if (hmgr.tnumber == 2)
                        {
                            return "Los Importes de las entradas en los IPV no coiciden con el importe de la salida en Ramal20.";


                        }
                        if (hmgr.tnumber == 3)
                        {
                            if (hmgr.atext.Count > 1)
                                return "Diríjase a la Ficha de Costo y aujste alli los importes de los productos para un valor de: $ " + hmgr.atext[1].ToString();
                            else
                                return "Diríjase a la Ficha de Costo y aujste alli los importes de los productos";


                        }
                    }
                    if (hmgr.errknd == ErrorKind.SM_Desc_Inicio2)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            if (hmgr.atext.Count > 0)
                                return "Tienes un Descuadre en el SubMayor de $ " + hmgr.atext[0].ToString();
                            else
                                return "Tienes un Descuadre en el SubMayor";

                        }
                        if (hmgr.tnumber == 2)
                        {
                            return "Tienes incoherencias en el inicio del Ramal20 con los finales del dia anterior.";


                        }
                        if (hmgr.tnumber == 3)
                        {
                            if (hmgr.atext.Count > 1)
                                return "Debido a que estos productos fueron desvinculados de esta cuenta o borrados de la base de datos: \n" + hmgr.atext[1];
                            else
                                return "Debido a que varios productos fueron desvinculados de esta cuenta o borrados de la base de datos.";


                        }
                        if (hmgr.tnumber == 4)
                        {
                          
                              return "Asócielos nuevamente en el Ramal20 o diríjase al DataFiller y alli corrija este error, incluyéndolos nuevamente, con los datos anteriores exactos.";


                        }
                    }
                    if (hmgr.errknd == ErrorKind.SM_Desc_Inicio3)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            if (hmgr.atext.Count > 0)
                                return "Tienes un Descuadre en el SubMayor de $ " + hmgr.atext[0].ToString();
                            else
                                return "Tienes un Descuadre en el SubMayor";

                        }
                        if (hmgr.tnumber == 2)
                        {
                            return "Tienes incoherencias en los inicios de los IPV con los finales del dia anterior.";


                        }
                        if (hmgr.tnumber == 3)
                        {
                            if (hmgr.atext.Count > 1)
                                return "Debido a que estos productos elaborados fueron cambiados o eliminados de la base de datos: \n" + hmgr.atext[1];
                            else
                                return "Debido a que estos productos elaborados fueron cambiados o eliminados de la base de datos.";


                        }
                        if (hmgr.tnumber == 4)
                        {

                            return "Configúrelos nuevamente, diríjase al DataFiller y allí corrija este error, incluyéndolos nuevamente, o corrigiendo los datos.";


                        }
                    }
                    if (hmgr.errknd == ErrorKind.SM_Desc_Prec)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            if (hmgr.atext.Count > 0)
                                return "Tienes un Descuadre en el SubMayor de $ " + hmgr.atext[0].ToString();
                            else
                                return "Tienes un Descuadre en el SubMayor";

                        }
                        if (hmgr.tnumber == 2)
                        {
                            return "Tienes diferencias entre los precios actuales y los del dia anterior en uno o mas Productos...";


                        }
                        if (hmgr.tnumber == 3)
                        {
                            if (hmgr.atext.Count > 1)
                                return "Los productos en conflicto son los siguientes: \n" + hmgr.atext[1];
                            else
                                return "Los productos en conflicto son los siguientes...";


                        }
                        if (hmgr.tnumber == 4)
                        {

                            return "Arregle el valor del Precio Unitario para dichos productos, diríjase al DataFiller y allí corrija este error o fuerce los precios en el Ramal20 del dia anterior.";


                        }
                    }
                    if (hmgr.errknd == ErrorKind.SM_Ramal_SubZero)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            if (hmgr.atext.Count > 0)
                                return "Tienes un Descuadre en el SubMayor de $ " + hmgr.atext[0].ToString();
                            else
                                return "Tienes un Descuadre en el SubMayor";

                        }
                        if (hmgr.tnumber == 2)
                        {
                            return "Existen productos en el Ramal20 de esta cuenta con valores de cantidad final menores que 0...";


                        }
                        if (hmgr.tnumber == 3)
                        {
                             if (hmgr.atext.Count > 1)
                                return "Los productos en conflicto son los siguientes: \n" + hmgr.atext[1];
                            else
                                return "Los productos en conflicto son los siguientes...";
                        }
                        if (hmgr.tnumber == 4)
                        {

                            return "Revise los finales de estos productos antes de continuar...";


                        }

                    }
                    if (hmgr.errknd == ErrorKind.SM_Desc_ChkIPV)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            if (hmgr.atext.Count > 0)
                                return "Tienes un Descuadre en el SubMayor de $ " + hmgr.atext[0].ToString();
                            else
                                return "Tienes un Descuadre en el SubMayor";

                        }
                        if (hmgr.tnumber == 2)
                        {
                            return "Faltan IPV por revisar...";


                        }
                        if (hmgr.tnumber == 3)
                        {
                            
                                return "Revise todos los IPV antes de continuar...";
                            

                        }
                       
                    }

                    if (hmgr.errknd == ErrorKind.SM_Desc_UnKnown)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            if (hmgr.atext.Count > 0)
                                return "Tienes un Descuadre en el SubMayor de $ " + hmgr.atext[0].ToString();
                            else
                                return "Tienes un Descuadre en el SubMayor";

                        }
                        if (hmgr.tnumber == 2)
                        {
                            if (hmgr.atext.Count > 0)
                                return "Tienes un Descuadre en el SubMayor de $ " + hmgr.atext[0].ToString();
                            else
                                return "Tienes un Descuadre en el SubMayor";
                        }
                        if (hmgr.tnumber == 3)
                        {
                            if (hmgr.atext.Count > 0)
                                return "Tienes un Descuadre en el SubMayor de $ " + hmgr.atext[0].ToString();
                            else
                                return "Tienes un Descuadre en el SubMayor";

                        }
                        
                    }
                }
                if (hmgr.topic == "Ramal20")
                {

                    if (hmgr.errknd == ErrorKind.RM_Wront_Prod)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            return "Ocurrió un Error a la hora de Insertar el nuevo Producto en el Ramal20...";
                        }
                        if (hmgr.tnumber == 2)
                        {
                            return "Asegúrese que el nombre del Producto que desea agregar sea correcto y que el Producto Exista...";
                        }
                        if (hmgr.tnumber == 3)
                        {
                            return "Asegúrese que el nombre del Producto que desea agregar No esté en conflicto con otros...";
                        }
                        if (hmgr.tnumber == 4)
                        {
                            return "Asegúrese que el Producto que desea agregar este asociado a esta cuenta: " + hmgr.atext[0] + ", y esta Moneda: " + hmgr.atext[1];
                        }
                    }

                }
                if (hmgr.topic == "IPV")
                {

                    if (hmgr.errknd == ErrorKind.IPV_Wront_Prod)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            return "Ocurrio un Error a la hora de Insertar el nuevo Producto Elaborado en el IPV...";
                        }
                        if (hmgr.tnumber == 2)
                        {
                            return "Asegúrese que el nombre del Producto que desea agregar sea correcto y que el Producto Exista...";
                        }
                        if (hmgr.tnumber == 3)
                        {
                            return "Asegúrese que el nombre del Producto que desea agregar No este en conflicto con otros...";
                        }
                        
                    }

                }
             if (hmgr.topic == "Ficha de Costo")
                {

                    if (hmgr.errknd == ErrorKind.FC_CHK_IPV)
                    {
                        if (hmgr.tnumber == 1)
                        {
                            return "La Ficha de Costo No muestra datos para esta cuenta...";
                        }
                        if (hmgr.tnumber == 2)
                        {
                            return "Sin embargo el sistema reconoce productos elaborados para esta cuenta...";
                        }
                        if (hmgr.tnumber == 3)
                        {
                            return "Asegúrese de haber revisado primero todos los IPV e intente de nuevo...";
                        }
                      
                    }

                }
            }
            return "La Ostia tío...!";
        }
    }
}
