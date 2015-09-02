﻿using ObjCRuntime;

namespace TrustDefender.iOS
{
  [Native]
  public enum THMStatusCode : long
  {
    NotYet = 0,
    Ok,
    ConnectionError,
    HostNotFoundError,
    NetworkTimeoutError,
    HostVerificationError,
    InternalError,
    InterruptedError,
    PartialProfile,
    InvalidOrgID
  }
}