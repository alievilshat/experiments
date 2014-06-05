<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_mailsettings')">
          <mailsettings>
            <mailsettingsid m:left="-143" m:top="155" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </mailsettingsid>
            <businessid m:left="-18" m:top="173" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="businessid" />
            </businessid>
            <email m:left="-152" m:top="190" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="email" />
            </email>
            <username m:left="-24" m:top="205" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="username" />
            </username>
            <password m:left="-154" m:top="222" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="password" />
            </password>
            <pop3server m:left="-27" m:top="240" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="pop3server" />
            </pop3server>
            <pop3port m:left="-155" m:top="256" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="pop3port" />
            </pop3port>
            <smtpserver m:left="-30" m:top="272" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="smtpserver" />
            </smtpserver>
            <smtpport m:left="-155" m:top="289" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="smtpport" />
            </smtpport>
            <usessl m:left="-32" m:top="306" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="usessl" />
            </usessl>
            <url m:left="-159" m:top="324" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="url" />
            </url>
            <leavecopy m:left="-35" m:top="341" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="leavecopy" />
            </leavecopy>
          </mailsettings>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>