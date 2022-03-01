using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace OsherProject
{
    class Sets
    {
        public static void SetEmail(TextBox t)
        {
            t.MaxLength = 50;
            EmailChecks.SetOnlyEmail(t);
        }
        public static void SetGeneral(TextBox t)
        {
            t.MaxLength = 40;
            Checks.SetGeneral(t);
        }
        public static void SetGeneral(ComboBox t)
        {
            t.MaxLength = 40;
            Checks.SetGeneral(t);
        }
        public static void SetID(TextBox t)
        {
            t.MaxLength = 9;
            Checks.SetOnlyID(t);
        }

        public static void SetID(ComboBox c)
        {
            c.MaxLength = 9;
            Checks.SetOnlyID(c);
        }

        public static void SetNumber(TextBox t)
        {
            t.MaxLength = 5;
            Checks.SetOnlyNumbers(t);
        }

        public static void SetPass(TextBox t)
        {
            t.MaxLength = 10;
            Checks.SetOnlyNumbers(t);
        }

        public static void SetNumber(ComboBox c)
        {
            c.MaxLength = 5;
            Checks.SetOnlyNumbers(c);
        }

        public static void SetPercent(TextBox t)
        {
            t.MaxLength = 2;
            Checks.SetOnlyNumbers(t);
        }

        public static void SetCarType(TextBox t)
        {
            t.MaxLength = 3;
            Checks.SetOnlyNumbers(t);
        }

        public static void SetCarModel(DateTimePicker t)
        {
            t.MinDate = DateTime.Today.AddYears(-30);
            t.MaxDate = DateTime.Today;
        }

        public static void SetCarNumber(TextBox t)
        {
            t.MaxLength = 9;
            Checks.SetOnlyCarNum(t);
        }
        public static void SetCarNumber(ComboBox t)
        {
            t.MaxLength = 9;
            Checks.SetOnlyCarNum(t);
        }

        public static void SetPayment(ComboBox c)
        {
            c.MaxLength = 20;
            Checks.SetOnlyHebrow(c);
        }

        public static void SetType(TextBox t)
        {
            t.MaxLength = 1;
            Checks.SetOnlyNumbers(t);
        }

        public static void SetPrice(TextBox t)
        {
            t.MaxLength = 9;
            Checks.SetOnlyNumbersAndPoint(t);
        }

        public static void SetName(TextBox t)
        {
            t.MaxLength = 20;
            Checks.SetOnlyHebrow(t);
        }
        public static void SetName(ComboBox c)
        {
            c.MaxLength = 20;
            Checks.SetOnlyHebrow(c);
        }

        public static void SetHouseNumber(TextBox t)
        {
            t.MaxLength = 4;
            Checks.SetOnlyAdressNum(t);
        }

        public static void SetPhone(TextBox t)
        {
            t.MaxLength = 7;
            Checks.SetOnlyNumbers(t);
        }

        public static void SetPrePhone(ComboBox c)
        {
            c.MaxLength = 3;
        }

        public static void SetZipCode(TextBox t)
        {
            t.MaxLength = 7;
            Checks.SetOnlyNumbers(t);
        }

        public static void SetDate(DateTimePicker d)
        {
            d.MaxDate = DateTime.Today;
        }

        public static void SetStartEndDate(DateTimePicker d)
        {
            d.MinDate = DateTime.Today;
        }

        public static void SetBirthDate(DateTimePicker d)
        {
            d.MaxDate = DateTime.Today.AddYears(-17);
        }

        public static void SetNote(TextBox t)
        {
            t.MaxLength = 50;
            Checks.SetOnlyHebrow(t);
        }

        public static void SetRole(ComboBox c)
        {
            c.MaxLength = 20;
            Checks.SetOnlyHebrow(c);
        }
    }
}
