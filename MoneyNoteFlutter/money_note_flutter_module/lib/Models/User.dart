import 'package:money_note_flutter_module/Models/ICommon.dart';

class User implements ICommon {
  String Id = '';
  String? Name;
  String? Email;
  String? Password;
  String? ConfirmPassword;

  User() {}
//
// public List<MoneyItem> MoneyItems { get; set; }
//
// public List<MainCategory> MainCategories { get; set; }
//
// public List<BankBook> BankBooks { get; set; }
//
// [Required]
// public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
//
// public DateTimeOffset UpdatedTime { get; set; }
//
// [Required]
// public bool IsApproved { get; set; } = false;
}
