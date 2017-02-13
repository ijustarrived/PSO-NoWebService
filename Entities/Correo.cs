using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSO.Entities
{
    public class Correo
    {
        public enum MailType
        {
            DOCS_INCOMPLETOS = 0,
            PEND_REVISAR,
            PEND_ASIG,
            PEND_ASIG_USER,
            PEND_TRABAJAR,
            PEND_TRABAJAR_USER,
            APROVADA,
            DENEGADA,
            REGISTER,
            PASS_RECOVER,
            PEND_REVISAR_USER,
            DOC_INCOMPLETOS_RESENT,
            DOC_INCOMPLETOS_USER
        }

        #region Subj and body definitions

        private const string DOCS_INCOMPLETOS_USER_BODY = @"<p>Estimado cliente:</p> 
<p>Su solicitud número @NUMSOLICITUD le ha sido devuelta por tener documentos incompletos. 
Específicamente: @COMMENTS.</p>
 
<p>Le solicitamos revise su solicitud y vuelva a enviárnosla a la brevedad posible para que la misma pueda ser procesada.</p>

<p>Agradecemos su interés en nuestros servicios y le mantendremos informado del progreso de su solicitud en 
cada etapa del proceso. Para cualquier duda o pregunta se puede comunicar al @PHONE de lunes a viernes,
8:00 a.m. a 5:00 p.m. o enviando un correo electrónico a <a>@CLIENTEMAIL</a> y con mucho gusto le ayudaremos.</p>

<p>Atentamente,</p>

<p>Servicio al Cliente @CLIENTE</p>

<p><u>Nota: Este es un correo electrónico enviado automáticamente. Favor no responder a este mensaje.</u><p>",

           DOC_INCOMPLETOS_RESENT_BODY = @"<p>Estimado cliente:</p>  
<p>Recibimos su solicitud número @NUMSOLICITUD servicio corregida. La misma estará siendo revisada
nuevamente por uno de nuestros coordinadores de servicio, quien se asegurará de que tenga todos
los documentos requeridos completos.  Una vez completada esta revisión, su solicitud se estará
pasando al Supervisor del Area de Servicio.</p>

<p>Agradecemos su interés en nuestros servicios y le mantendremos informado del progreso de su solicitud en 
cada etapa del proceso. Para cualquier duda o pregunta se puede comunicar al @PHONE de lunes a viernes,
8:00 a.m. a 5:00 p.m. o enviando un correo electrónico a <a>@CLIENTEMAIL</a> y con mucho gusto le ayudaremos.</p>

<p>Atentamente,</p>

<p>Servicio al Cliente @CLIENTE</p>

<p><u>Nota: Este es un correo electrónico enviado automáticamente. Favor no responder a este mensaje.</u></p>",

           PEND_REVISAR_BODY = @"Recibimos una nueva solicitud de @TYPE servicio. Se le asignó 
el número @NUMSOLICITUD a dicha solicitud. ",

           PEND_ASIG_BODY = @"La solicitud número @NUMSOLICITUD ya fue revisada como completa por uno de 
nuestros coordinadores de servicio. Se le acaba de pasar a usted para su revisión y para que se la asigne a 
un Procesador para que la trabaje.",

            DOCS_INCOMPLETOS_BODY = @"Recibimos la solicitud de servicio número @NUMSOLICITUD corregida para ser 
revisada nuevamente.",

           PEND_ASIG_USER_BODY = @"<p>Estimado cliente:</p>
<p>Su solicitud número @NUMSOLICITUD ya fue revisada como completa por uno de nuestros 
coordinadores de servicio. La misma se pasó a la revisión del Supervisor del Area de 
Servicio para que la revise y le asigne un Procesador para que la trabaje.</p>

<p>Agradecemos su interés en nuestros servicios y le mantendremos informado del progreso de su solicitud en 
cada etapa del proceso. Para cualquier duda o pregunta se puede comunicar al @PHONE de lunes a viernes,
8:00 a.m. a 5:00 p.m. o enviando un correo electrónico a <a>@CLIENTEMAIL</a> y con mucho gusto le ayudaremos.</p>

<p>Atentamente,</p>

<p>Servicio al Cliente @CLIENTE</p>

<p><u>Nota: Este es un correo electrónico enviado automáticamente. Favor no responder a este mensaje.</u></p>",

           PEND_TRABAJAR_BODY = @"La solicitud número @NUMSOLICITUD ya fue revisada por el Supervisor de 
Procesadores. Se le acaba de asignar usted para que la trabaje.",

           PEND_TRABAJAR_USER_BODY = @"<p>Estimado cliente:</p>  
<p>Su solicitud número @NUMSOLICITUD ya fue revisada por el Supervisor de Procesadores y asignó 
la misma a un Procesador para que la trabaje. Una vez el Procesador la trabaje le estaremos 
dejando saber si su solicitud fue Aprobada o Denegada.</p>

<p>Agradecemos su interés en nuestros servicios y le mantendremos informado del progreso de su solicitud en 
cada etapa del proceso. Para cualquier duda o pregunta se puede comunicar al @PHONE de lunes a viernes,
8:00 a.m. a 5:00 p.m. o enviando un correo electrónico a <a>@CLIENTEMAIL</a> y con mucho gusto le ayudaremos.</p>

<p>Atentamente,</p>

<p>Servicio al Cliente @CLIENTE</p>

<p><u>Nota: Este es un correo electrónico enviado automáticamente. Favor no responder a este mensaje.</u></p>",

           APROVADA_USER_BODY = @"<p>Estimado cliente:</p>
<p>Nos place informarle que su solicitud número @NUMSOLICITUD fue aprobada. 

<p>Puede pasar por nuestras oficinas para recoger su cheque a partir de mañana a las 8:00 a.m.</p>

<p>Nos place haber podido servirle y nos encontramos a su disposición de usted requerir algún servicio adicional.</p>

<p>Atentamente,</p>

<p>Servicio al Cliente @CLIENTE</p>

<p><u>Nota: Este es un correo electrónico enviado automáticamente. Favor no responder a este mensaje.</u></p>",

           DENEGADA_USER_BODY = @"<p>Estimado cliente:</p>  
<p>Lamentamos informarle que su solicitud número @NUMSOLICITUD fue denegada. 
Estará recibiendo por correo una carta con el detalle de las razones específicas de la denegación.</p> 

<p>Para cualquier duda o pregunta se puede comunicar al @PHONE de lunes a viernes,
8:00 a.m. a 5:00 p.m. o enviando un correo electrónico a <a>@CLIENTEMAIL</a> y con mucho gusto le ayudaremos.</p>

<p>Atentamente,</p>

<p>Servicio al Cliente @CLIENTE</p>

<p><u>Nota: Este es un correo electrónico enviado automáticamente. Favor no responder a este mensaje.</u></p>",

           REGISTER_BODY = @"<p>Estimado cliente:</p>
<p>Bienvenido al Portal de Servicios de Cliente @CLIENT.</p>

<p>Su usuario ha sido creado exitosamente. Desde este momento, usted podrá acceder al 
sistema para solicitar los servicios que requiera de manera simple, fácil y segura.
Recuerde que siempre nos encontramos a su disposición para cualquier servicio que pueda 
necesitar. Para cualquier duda o pregunta se puede comunicar al @PHONE de lunes a 
viernes, 8:00 a.m. a 5:00 p.m. o enviando un correo electrónico a <a>@CLIENTEMAIL</a> y con mucho gusto le ayudaremos.</p>

<p>Atentamente,</p>

<p>Servicio al Cliente @CLIENTE</p>

<p><u>Nota: Este es un correo electrónico enviado automáticamente. Favor no responder a este mensaje.</u></p> ",

           PASS_RECOVER_BODY = @"<p>Estimado cliente:</p>
<p>Recibimos su solicitud de cambio de contraseña. A continuación su contraseña.</p> 

<p>Su contraseña es: @PASS.</p>

<p>Gracias por ser un cliente de @CLIENT. Para cualquier duda o pregunta se puede comunicar 
al @PHONE de lunes a viernes, 8:00 a.m. a 5:00 p.m. o enviando un correo electrónico a 
<a>@CLIENTEMAIL</a> y con mucho gusto le ayudaremos.</p>
 
<p>Atentamente,</p> 

<p>Servicio al Cliente @CLIENT</p>
 
<p>Este es un correo electrónico enviado automáticamente. Favor no responder al mensaje.</p>",

           PEND_REVISAR_USER_BODY = @"<p>Estimado cliente:</p>
<p>Recibimos su solicitud de @TYPE servicio. Se le asignó el número @NUMSOLICITUD a dicha solicitud.
La misma estará siendo revisada por uno de nuestros coordinadores de servicio, quien se asegurará 
de que tenga todos los documentos requeridos completos. Una vez completada esta revisión, su solicitud 
se estará pasando al Supervisor del Área de Servicio.</p>

<p>Agradecemos su interés en nuestros servicios y le mantendremos informado del progreso de su solicitud en 
cada etapa del proceso. Para cualquier duda o pregunta se puede comunicar al @PHONE de lunes a viernes,
8:00 a.m. a 5:00 p.m. o enviando un correo electrónico a <a>@CLIENTEMAIL</a> y con mucho gusto le ayudaremos.</p>

<p>Atentamente,<p>

<p>Servicio al Cliente @CLIENTE</p>

<p><u>Nota: Este es un correo electrónico enviado automáticamente. Favor no responder a este mensaje.</u><p>",

           DOCS_INCOMPLETOS_USER_SUBJECT = "Solicitud de Nuevo Servicio Devuelta",
            DOCS_INCOMPLETOS_SUBJ = "Solicitud de Nuevo Servicio Corregida fue Recibida",
           DOC_INCOMPLETOS_RESENT_SUBJ = "Solicitud de Nuevo Servicio Corregida fue Recibida",
           PEND_REVISAR_SUBJECT = "Solicitud de Nuevo Servicio fue Recibida",
           PEND_ASIG_SUBJECT = "Nueva Solicitud de Servicio Lista para ser Revisada y Asignada",
           PEND_ASIG_USER_SUBJECT = "Solicitud de Nuevo Servicio pasó al Supervisor de Procesadores ",
           PEND_TRABAJAR_SUBJECT = "Nueva Solicitud de Servicio Lista para Trabajarse",
           PEND_TRABAJAR_USER_SUBJECT = "Solicitud de Nuevo Servicio pasó al Procesador de Servicio ",
           APROVADA_USER_SUBJECT = "Solicitud de Nuevo Servicio fue Aprobada",
           DENEGADA_USER_SUBJECT = "Solicitud de Nuevo Servicio fue Denegada",
           REGISTER_SUBJECT = "Registro de su Cuenta",
           PASS_RECOVER_SUBJ = "Recuperación de Contraseña",
           PEND_REVISAR_USER_SBJ = "Solicitud de Nuevo Servicio fue Recibida";

        #endregion

        public static string GetSubject(MailType mailType)
        {
            string subj = string.Empty;

            switch (mailType)
            {
                case MailType.APROVADA:

                    subj = APROVADA_USER_SUBJECT;

                    break;

                case MailType.DENEGADA:

                    subj = DENEGADA_USER_SUBJECT;

                    break;

                case MailType.DOC_INCOMPLETOS_USER:

                    subj = DOCS_INCOMPLETOS_USER_SUBJECT;

                    break;

                case MailType.DOCS_INCOMPLETOS:

                    subj = DOCS_INCOMPLETOS_SUBJ;

                    break;

                case MailType.PEND_ASIG:

                    subj = PEND_ASIG_SUBJECT;

                    break;

                case MailType.PEND_ASIG_USER:

                    subj = PEND_ASIG_USER_SUBJECT;

                    break;

                case MailType.PEND_REVISAR:

                    subj = PEND_REVISAR_SUBJECT;

                    break;

                case MailType.PEND_TRABAJAR:

                    subj = PEND_TRABAJAR_SUBJECT;

                    break;

                case MailType.PEND_TRABAJAR_USER:

                    subj = PEND_TRABAJAR_USER_SUBJECT;

                    break;

                case MailType.DOC_INCOMPLETOS_RESENT:

                    subj = DOC_INCOMPLETOS_RESENT_SUBJ;

                    break;

                case MailType.PASS_RECOVER:

                    subj = PASS_RECOVER_SUBJ;

                    break;

                case MailType.PEND_REVISAR_USER:

                    subj = PEND_REVISAR_USER_SBJ;

                    break;

                case MailType.REGISTER:

                    subj = REGISTER_SUBJECT;

                    break;
            }

            return subj;
        }

        public static string GetBody(MailType mailType)
        {
            string body = string.Empty;

            switch (mailType)
            {
                case MailType.APROVADA:

                    body = APROVADA_USER_BODY;

                    break;

                case MailType.DENEGADA:

                    body = DENEGADA_USER_BODY;

                    break;

                case MailType.DOC_INCOMPLETOS_USER:

                    body = DOCS_INCOMPLETOS_USER_BODY;

                    break;

                case MailType.DOCS_INCOMPLETOS:

                    body = DOCS_INCOMPLETOS_BODY;

                    break;

                case MailType.PEND_ASIG:

                    body = PEND_ASIG_BODY;

                    break;

                case MailType.PEND_ASIG_USER:

                    body = PEND_ASIG_USER_BODY;

                    break;

                case MailType.PEND_REVISAR:

                    body = PEND_REVISAR_BODY;

                    break;

                case MailType.PEND_TRABAJAR:

                    body = PEND_TRABAJAR_BODY;

                    break;

                case MailType.PEND_TRABAJAR_USER:

                    body = PEND_TRABAJAR_USER_BODY;

                    break;

                case MailType.DOC_INCOMPLETOS_RESENT:

                    body = DOC_INCOMPLETOS_RESENT_BODY;

                    break;

                case MailType.PASS_RECOVER:

                    body = PASS_RECOVER_BODY;

                    break;

                case MailType.PEND_REVISAR_USER:

                    body = PEND_REVISAR_USER_BODY;

                    break;

                case MailType.REGISTER:

                    body = REGISTER_BODY;

                    break;
            }

            return body;
        }
    }
}