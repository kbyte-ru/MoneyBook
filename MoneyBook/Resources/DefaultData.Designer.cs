﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18444
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MoneyBook.Resources {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DefaultData {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DefaultData() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MoneyBook.Resources.DefaultData", typeof(DefaultData).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Кредитная карта|0
        ///Банковский счёт|0
        ///Наличные|0
        ///WebMoney|0
        ///Яндекс.Деньги|0
        ///PayPal|0.
        /// </summary>
        internal static string AccountTypes {
            get {
                return ResourceManager.GetString("AccountTypes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на .
        /// </summary>
        internal static string Categories {
            get {
                return ResourceManager.GetString("Categories", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на RUB|Российский рубль|₽|0
        ///BYR|Белорусский рубль|Br|1
        ///KZT|Казахстанский тенге|KZT|2
        ///UAH|Украинская гривна|грн.|3
        ///USD|Американский доллар|$|10
        ///EUR|Евро|€|11
        ///CNY|Китайский Юань|CNY|12
        ///GBP|Британский фунт|£|13
        ///JPY|Японская Йена|¥|14.
        /// </summary>
        internal static string Currencies {
            get {
                return ResourceManager.GetString("Currencies", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на RUB|Основной|3|0.
        /// </summary>
        internal static string DefaultAccount {
            get {
                return ResourceManager.GetString("DefaultAccount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на RUB.
        /// </summary>
        internal static string DefaultCurrency {
            get {
                return ResourceManager.GetString("DefaultCurrency", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на 1|&lt;Разные расходы&gt;|#000000|#F3F3F3|0
        ///2|Услуги связи|#000000|#CAEEFF|0
        ///3|Транспортные расходы|#000000|#FFCC99|0
        ///4|Продукты питания|#000000|#CCFFCC|0
        ///5|Покупки|#000000|#FFFFFF|0
        ///6|Коммунальные платежи|#000000|#E4CBCB|0
        ///7|Образование, воспитание|#000000|#9DFFFF|0
        ///8|Красота и здоровье|#000000|#CBE4E4|0
        ///9|Долги/Кредиты|#000000|#FAE79A|0
        ///10|Налоги|#000000|#FFFFFF|0
        ///11|Штрафы|#000000|#FFFFFF|0
        ///12|Развлечения|#000000|#FFCAE4|0
        ///13|Подарки|#000000|#FFBFFF|0
        ///14|Благотворительность|#000000#|#FFFFFF|0
        ///15|П [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string Expenses {
            get {
                return ResourceManager.GetString("Expenses", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на 1|&lt;Разные доходы&gt;|#000000|#F3F3F3|0
        ///2|Зарплата|#000000|#FFFF00|0
        ///3|Шальные деньги|#000000|#AEE4FF|0
        ///4|Подарки, выигрыш|#000000|#FFB3D9|0
        ///5|Возвращен долг|#000000|#DDBBFF|0
        ///6|Взято в долг|#000000|#FFBBBB|0
        ///7|Обмен валют|#000000|#FFFFFF|0
        ///8|Перевод со счета|#000000|#FFFFFF|0.
        /// </summary>
        internal static string Incomes {
            get {
                return ResourceManager.GetString("Incomes", resourceCulture);
            }
        }
    }
}
