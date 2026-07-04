#pragma once

constexpr Label::Label(std::string_view name)
    : _name(name)
{
}

constexpr Label::Label(std::string_view name, Identifier id)
    : _uid(id), _name(name)
{
}
