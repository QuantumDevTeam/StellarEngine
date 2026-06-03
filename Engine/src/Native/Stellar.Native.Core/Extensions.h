#pragma once

// pragma

#define STELLAR_PRAGMA(x) _Pragma(#x)

#define STELLAR_CLANG_IGNORE_START()\
STELLAR_PRAGMA(clang diagnostic push)

#define STELLAR_CLANG_IGNORE_ADD(which)\
STELLAR_PRAGMA(clang diagnostic ignored which)

#define STELLAR_CLANG_IGNORE_END()\
STELLAR_PRAGMA(clang diagnostic pop)

#define STELLAR_CLANG_IGNORE(which)\
STELLAR_CLANG_IGNORE_START() \
STELLAR_CLANG_IGNORE_ADD(which)

// class and struct members

#define PropertySetter(Type, Name)\
void Set##Name(const Type& value)

#define PropertyGetter(Type, Name)\
[[nodiscard]] const Type& Get##Name() const

#define STELLAR_CONSTRUCT(Type)\
Type() = default

#define STELLAR_DECONSTRUCT(Type, modifier)\
~Type() modifier = default

#define STELLAR_CONSTRUCTION(Type, modifier, secondModifier)\
STELLAR_CONSTRUCT(Type);\
modifier STELLAR_DECONSTRUCT(Type, secondModifier)

#define STELLAR_DEFAULT_COPY_OPERATORS(Type)\
Type(const Type&) = default;\
Type(Type&&) noexcept = default;\
Type& operator=(const Type&) = default;\
Type& operator=(Type&&) noexcept = default

#define STELLAR_DELETE_COPY_OPERATORS(Type)\
Type(const Type&) = delete;\
Type& operator=(const Type&) = delete;\
Type(Type&&) = delete;\
Type& operator=(Type&&) = delete

#define STELLAR_CLASS_NAME_DEF(Type, modifier, secondModifier, thirdModifyer)\
    modifier static const char* StaticClassName() { return "Native."#Type ; }\
    modifier secondModifier const char* GetClassName() const thirdModifyer { return StaticClassName(); }

#define STELLAR_TO_STRING()\
std::string ToString() const noexcept

#define STELLAR_HASHCODE()\
uint64_t GetHashCode() const noexcept

#define STELLAR_SPACESHIP(Type)\
auto operator<=>(const Type&) const noexcept = default

#define STELLAR_DEFAULTS(Type, modifier, secondModifier)\
STELLAR_CLASS_NAME_DEF(Type, , modifier , secondModifier)\
[[nodiscard]] modifier STELLAR_TO_STRING() secondModifier;\
[[nodiscard]] modifier STELLAR_HASHCODE() secondModifier;\
STELLAR_SPACESHIP(Type);

#define STELLAR_INLINE_DEFAULTS(Type, modifier, secondModifier)\
STELLAR_CLASS_NAME_DEF(Type, , modifier , secondModifier)\
[[nodiscard]] modifier STELLAR_TO_STRING() secondModifier { return std::format("{}#UID({})", Type::StaticClassName(), to_string(GetUID())); }\
[[nodiscard]] modifier STELLAR_HASHCODE() secondModifier { return GetUID().GetHashCode(); }\
STELLAR_SPACESHIP(Type);

#define STELLAR_INLINE_UID(id)\
[[nodiscard]] constexpr const Stellar::Native::Core::Identifier& GetUID() const id

#define STELLAR_INLINE_LABEL(lbl)\
[[nodiscard]] constexpr const Stellar::Native::Core::Label& GetLabel() const lbl

#define STELLAR_GENERATE_BODY_PARTIAL(Type, modifier, secondModifier, thirdModifier)\
    modifier STELLAR_CONSTRUCTION(Type, secondModifier, thirdModifier);\
public:\
    STELLAR_DEFAULT_COPY_OPERATORS(Type);

#define STELLAR_GENERATE_BODY_FLAGGED(Type, BaseType, modifier, secondModifier, thirdModifier)\
using Base = BaseType;\
using Base::Base;\
STELLAR_GENERATE_BODY_PARTIAL(Type, modifier, secondModifier, thirdModifier)

#define STELLAR_GENERATE_BODY(Type, BaseType)\
using Base = BaseType;\
using Base::Base;\
STELLAR_GENERATE_BODY_PARTIAL(Type, constexpr, , noexcept override)

#define STELLAR_GENERATE_INTERFACE(Type)\
public: STELLAR_GENERATE_BODY_PARTIAL(Type, constexpr, virtual)

#define STELLAR_GENERATE_SINGLETON(Type, modifier, secondModifier)\
private:\
    STELLAR_GENERATE_BODY_PARTIAL(Type, modifier, secondModifier)\
public:\
    modifier static Type& GetInstance() {\
        static Type instance;\
        return instance;\
    }

#define STELLAR_GENERATE_QUANT(Type, BaseType)\
STELLAR_GENERATE_BODY(Type, BaseType)

#define STELLAR_GENERATE_TO_STRING(Type)\
[[nodiscard]] inline std::string to_string(const Type& obj) noexcept { return obj.ToString(); }

#define STELLAR_GENERATE_HASHER(FullType)\
template <>\
struct std::hash<FullType>\
{\
    size_t operator()(const FullType& obj) const noexcept\
    {\
        return obj.GetHashCode();\
    }\
};

#define STELLAR_GENERATE_DEFAULTS(Namespace, Type)\
namespace Namespace\
{\
    STELLAR_GENERATE_TO_STRING(Type)\
}\
\
STELLAR_GENERATE_HASHER(Namespace::Type)
