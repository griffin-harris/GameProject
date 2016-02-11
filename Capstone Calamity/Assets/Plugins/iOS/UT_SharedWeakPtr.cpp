#include "UT_SharedWeakPtr.h"

UT_SharedWeakPtrHolder::UT_SharedWeakPtrHolder(UT_SharedWeakBase *ptr)
: _ptr(ptr)
{
}

UT_SharedWeakPtrHolder::~UT_SharedWeakPtrHolder()
{
}

////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////

UT_SharedWeakPtrBase::UT_SharedWeakPtrBase()
: _holder()
{
}

UT_SharedWeakPtrBase::UT_SharedWeakPtrBase(const UT_SharedWeakPtrBase& ptr)
: _holder(ptr._holder)
{
}

////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////

UT_SharedWeakPtrImpl::UT_SharedWeakPtrImpl()
{
}

UT_SharedWeakPtrImpl::~UT_SharedWeakPtrImpl()
{
}

void UT_SharedWeakPtrImpl::setPtr(UT_SharedWeakBase *ptr)
{
  if(_holder.get() == NULL)
    _holder = new UT_SharedWeakPtrHolder(ptr);
  _holder->_ptr = ptr;
}

////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////

UT_SharedWeakBase::UT_SharedWeakBase()
{
  _weakPtr.setPtr( this );
}

UT_SharedWeakBase::~UT_SharedWeakBase()
{
  _weakPtr.setPtr(0);
}