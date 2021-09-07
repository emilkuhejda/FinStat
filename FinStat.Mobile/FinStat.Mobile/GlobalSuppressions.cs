// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "By design", Scope = "member", Target = "~M:FinStat.Mobile.ViewModels.ViewModelBase.Dispose(System.Boolean)")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "It is disposed with caller", Scope = "member", Target = "~M:FinStat.Mobile.Utils.ThreadHelper.InvokeOnUiThread(System.Action)")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "It is disposed with caller", Scope = "member", Target = "~M:FinStat.Mobile.Utils.ThreadHelper.InvokeOnUiThread``1(System.Func{``0})~``0")]
